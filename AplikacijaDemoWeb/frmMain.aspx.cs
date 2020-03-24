using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Text;

namespace AplikacijaDemoWeb
{
    public partial class frmMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            if (!IsPostBack)
            {
                using (var db = new DemoEntities())
                {
                    GridView.DataSource = db.DohvatiPrimatelje();
                    GridView.DataBind();
                }
                string filepath = Server.MapPath("~/sms/");
                if (!Directory.Exists(filepath))
                {
                    lblCharCount.Text = "Ne postoji sms folder na lokaciji projekta. Slanje poruka nije omogućeno!";
                    lblCharCount.Font.Bold = true;
                    lblCharCount.ForeColor = System.Drawing.Color.Red;
                    txtSMSMessage.Visible = false;
                    btnPosalji.Enabled = false;
                }
                else
                {
                    txtSMSMessage.Visible = true;
                    btnPosalji.Enabled = true;
                }
            }
        }

        protected void btnDodajPrimatelja_Click(object sender, EventArgs e)
        {
            //provjeriti da li su popunjena polja ImePrezime i BrojMobitela i da li su ispravna
            string imePrezime = txtImePrezime.Text.Trim();
            string brojMobitela = txtBrojMobitela.Text.Trim();
            if (!VerifyUtil.verifyImePrezime(imePrezime))
            {
                lblDodajPrimateljaError.Text = "Ime i prezime mora biti popunjeno!";
                lblDodajPrimateljaError.Visible = true;
                return;
            }

            if (!VerifyUtil.verifyBrojMobitela(brojMobitela))
            {
                lblDodajPrimateljaError.Text = "Broj mobitela nije ispravno popunjen!";
                lblDodajPrimateljaError.Visible = true;
                return;
            }

            int result = -1;
            // pozvati proceduru za upis podataka
            using (var db = new DemoEntities())
            {
                result = db.DodajPrimatelja(imePrezime, brojMobitela);
                GridView.DataSource = db.DohvatiPrimatelje();
                GridView.DataBind();
            }

            //provjeriti da li je podatak ispravno upisan
            if (result != 1)
            {
                lblDodajPrimateljaError.Text = "Primatelj nije unesen, vjerojatno ste upisali postojeći broj mobitela!";
                lblDodajPrimateljaError.Visible = true;
                return;
            }

            //obrisati upisane vrijednosti
            txtImePrezime.Text = "";
            txtBrojMobitela.Text = "";
            lblDodajPrimateljaError.Text = "";
            lblDodajPrimateljaError.Visible = false;
        }

        protected void btnPosalji_Click(object sender, EventArgs e)
        {
            lblSentSuccess.Text = "";
            int brojPrimatelja = 0;
            string poruka = txtSMSMessage.Text.Trim();
            if (poruka.Length > 0)
            {
                if (poruka.Length < 161)
                {
                    StringBuilder obavijest = new StringBuilder("");
                    Stack primatelji = new Stack();
                    CheckBox chk;
                    foreach (GridViewRow item in GridView.Rows)
                    {
                        chk = (CheckBox)item.FindControl("cbSelect");
                        if (chk.Checked)
                        {
                            primatelji.Push(item);
                        }
                    }
                    brojPrimatelja = primatelji.Count;
                    if (brojPrimatelja > 0)
                    {
                        int sent = PosaljiPrimateljima(primatelji, poruka);
                        if (sent == brojPrimatelja)
                        {
                            txtSMSMessage.Text = "";
                            obavijest.Append("Poruke su uspješno poslane.");
                        }
                        else
                            obavijest.Append("Poslano ").Append(sent).Append(" od ").Append(brojPrimatelja).Append(" poruka.");
                    }
                    else
                    {
                        obavijest.Append("Nisu odabrani primatelji poruke!");
                    }
                    lblSentSuccess.Text = obavijest.ToString();
                }
                else
                {
                    using (var db = new DemoEntities())
                    {
                        db.DodajLogZapis(DateTime.Now, (int)Const.Akcije.UPOZORENJE_PREDUGA_PORUKA, "", new StringBuilder("Pokušaj slanja poruke dužine ").Append(poruka.Length).ToString());
                    }
                }
            }
        }

        private int PosaljiPrimateljima(Stack primatelji, string tekst)
        {
            string filePrefix = "demo_";
            string fileSufix = ".txt";
            string imePrezime;
            string brojMobitela;
            string filename;
            DateTime datum = DateTime.Now;
            CheckBox chk;
            int success = 0;

            while (primatelji.Count > 0)
            {
                using (GridViewRow redak = (GridViewRow)primatelji.Pop())
                {
                    imePrezime = redak.Cells[1].Text;
                    brojMobitela = redak.Cells[2].Text;
                    filename = filePrefix + brojMobitela + fileSufix;
                    string filepath = Server.MapPath("~/sms/") + filename;
                    if (SendSMSUtils.posaljiPoruku(filepath, tekst))
                    {
                        if (SendSMSUtils.evidentirajPoruku(imePrezime, brojMobitela, filename, datum))
                        {
                            chk = (CheckBox)redak.FindControl("cbSelect");
                            chk.Checked = false;
                            success++;
                        }
                        else
                        {
                            SendSMSUtils.obrisiPoruku(filepath);
                        }
                    }
                }
            }
            return success;
        }
    }
}