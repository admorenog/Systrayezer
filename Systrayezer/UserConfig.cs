using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systrayezer
{
    class UserConfig
    {
        private string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string appName = "Systrayezer";
        private string configFileExtension = ".cfg";
        private static string configAppPath = "";
        private static ConfigSetting[] config;
        
        public UserConfig()
        {
            configAppPath = configPath + Path.DirectorySeparatorChar + appName + configFileExtension;
            
            config = ReadConfig(configAppPath);
        }

        private ConfigSetting[] ReadConfig(string configAppPath)
        {
            config = new ConfigSetting[0];

            if (File.Exists(configAppPath))
            {
                // TODO: change for read json or xml config
                string[] configLines = File.ReadAllLines(configAppPath);
                config = new ConfigSetting[configLines.Length];
                for(int idxConfigLine = 0; idxConfigLine < configLines.Length; idxConfigLine++)
                {
                    config[idxConfigLine] = new ConfigSetting(configLines[idxConfigLine]);
                }
            }

            return config;
        }
    }
}
