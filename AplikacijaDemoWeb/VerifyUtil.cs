using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AplikacijaDemoWeb
{
    public abstract class VerifyUtil
    {
        public static bool verifyImePrezime(string imePrezime)
        {
            return !string.IsNullOrEmpty(imePrezime);
        }

        public static bool verifyBrojMobitela(string brojMobitela)
        {
            string strRegex = @"(^\d{11,12}$)"; //@"(^[0-9]{11,12}$)";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(brojMobitela))
                return (true);
            else
                return (false);
        }

    }
}