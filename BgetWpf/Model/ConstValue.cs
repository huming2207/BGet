using System.IO;
using System.Reflection;


namespace BgetWpf.Model
{
    public class ConstValue
    {
        public const string DefaultUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";

        public static string SettingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                           "/setting.json";
    }
}
