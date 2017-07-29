using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BgetWpf.Controller
{
    public class InitialConfig
    {
        public static void InitConfig()
        {
            if (Properties.Settings.Default.ResetConfig)
            {
                Properties.Settings.Default.Reset();
            }
        }
    }
}
