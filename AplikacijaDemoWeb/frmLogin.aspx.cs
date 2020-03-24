using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
namespace AplikacijaDemoWeb
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (ValidateUser(sender, e))
            {
                FormsAuthentication.RedirectFromLoginPage(Login.UserName, Login.RememberMeSet);
            }
        }
        protected Boolean ValidateUser(object sender, EventArgs e)
        {

            // Retrieve the partial connection string named databaseConnection
            // from the application's app.config or web.config file.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["connString"];

            if (null != settings)
            {
                // Retrieve the partial connection string.
                string connectString = settings.ConnectionString;
                Console.WriteLine("Original: {0}", connectString);

                // Create a new SqlConnectionStringBuilder based on the
                // partial connection string retrieved from the config file.
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);

                // Supply the additional values.
                builder.UserID = Login.UserName;
                builder.Password = Login.Password;


                using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                {
                    try
                    {
                        con.Open();
                    }
                    catch (SqlException)
                    {
                        Login.FailureText = "Neispravna prijava za korisnika " + Login.UserName;
                        return false;
                    }
                    catch (Exception)
                    {
                        Login.FailureText = "Ispričavamo se, došlo je do nepreviđenog problema u radu aplikacije.";
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}