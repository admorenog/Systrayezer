using System.Linq;
using System.Xml.Linq;

namespace Systrayezer
{
    class ConfigSetting
    {
        public ConfigSetting(string type, XElement configLine)
        {
            if(type == "binding")
            {
                string control = configLine.Elements().Where(x => x.Name == "control").First().Value;
                string key = configLine.Elements().Where(x => x.Name == "key").First().Value;
                string app = configLine.Elements().Where(x => x.Name == "app").First().Value;
            }
        }
    }
}
