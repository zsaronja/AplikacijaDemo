using System;
using System.IO;
using System.Text;

namespace AplikacijaDemoWeb
{
    public abstract class SendSMSUtils
    {

        public static bool evidentirajPoruku(string imePrezime, string brojMobitela, string filename, DateTime datum)
        {
            // pozvati proceduru za upis podataka
            try
            {
                using (var db = new DemoEntities())
                {
                    db.UpisiPoruku(datum, imePrezime, brojMobitela, filename);
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// Simulira slanje SMS poruke.
        /// Kreira datoteku na zadanoj lokaciji i upisuje tekst poruke.
        /// </summary>
        /// <param name="filepath">putanja datoteke</param>
        /// <param name="tekst">tekst poruke</param>
        /// <returns>true ako je poruka uspješno poslana, false ako nije</returns>
        public static bool posaljiPoruku(string filepath, string tekst)
        {
            try
            {
                // ako datoteka već postoji, obriši ju. Bolje rješenje je napraviti kopiju s drugim imenom ili dodati u naziv datoteke i vrijeme kreiranja da se izbjegnu duplikati
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                // Kreiraj datoteku i upiši poruku
                using (FileStream fs = File.Create(filepath))
                {
                    byte[] byFileName = new UTF8Encoding(true).GetBytes(tekst);
                    fs.Write(byFileName, 0, byFileName.Length);
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool obrisiPoruku(string filepath)
        {
            try
            {
                // obrisi datoteku - ima smisla ako se ne salje odmah po stvaranju
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}