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
    }
}
