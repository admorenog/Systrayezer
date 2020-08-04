using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static ConfigSettings config;
        
        public UserConfig()
        {
            configAppPath = configPath + Path.DirectorySeparatorChar + appName + Path.DirectorySeparatorChar + configFileName + configFileExtension;
            
            config = ReadConfig(configAppPath);
        }

        private ConfigSettings ReadConfig(string configFilePath)
        {
            config = new ConfigSettings();

            if (File.Exists(configFilePath))
            {
                XElement doc = XElement.Load(configFilePath);
                IEnumerable<XElement> bindings = doc.Elements("bindings").Elements();

                int countOfBindings = bindings.Count();

                config = new ConfigSettings();
                for(int idxConfigLine = 0; idxConfigLine < countOfBindings; idxConfigLine++)
                {
                    config.Add(new Config.Binding(bindings.ElementAt(idxConfigLine)));
                }
            }

            return config;
        }
    }
}
