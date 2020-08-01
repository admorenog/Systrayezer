using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Systrayezer
{
    class UserConfig
    {
        private string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string appName = "Systrayezer";
        private string configFileName = "settings";
        private string configFileExtension = ".xml";
        private static string configAppPath = "";
        private static ConfigSetting[] config;
        
        public UserConfig()
        {
            configAppPath = configPath + Path.DirectorySeparatorChar + appName + Path.DirectorySeparatorChar + configFileName + configFileExtension;
            
            config = ReadConfig(configAppPath);
        }

        private ConfigSetting[] ReadConfig(string configFilePath)
        {
            config = new ConfigSetting[0];

            if (File.Exists(configFilePath))
            {
                // TODO: change for read json or xml config
                XElement doc = XElement.Load(configFilePath);
                IEnumerable<XElement> bindings = doc.Elements("bindings").Elements();

                int countOfBindings = bindings.Count<XElement>();

                config = new ConfigSetting[countOfBindings];
                for(int idxConfigLine = 0; idxConfigLine < countOfBindings; idxConfigLine++)
                {
                    config[idxConfigLine] = new ConfigSetting("binding", bindings.ElementAt(idxConfigLine));
                }
            }

            return config;
        }
    }
}
