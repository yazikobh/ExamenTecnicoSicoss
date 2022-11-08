using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SicossLectorWebConfig
{
    public static class VariablesWebConfig
    {
        public static string ServerURL => ConfigurationManager.AppSettings["ServerURL"];
        public static bool SessionAsCookie
        {
            get
            {
                return ConfigurationManager.AppSettings["SessionAsCookie"] == "true";
            }
        }
        public static int SessionCookieTime
        {
            get
            {
                int result = 12;
                if (!int.TryParse(ConfigurationManager.AppSettings["SessionCookieTime"], out result))
                    result = 12;
                return result;
            }
        }
    }
}
