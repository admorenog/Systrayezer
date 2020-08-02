using System.Collections.ObjectModel;
using Systrayezer.Config;

namespace Systrayezer
{
    class ConfigSettings
    {
        public const int TypeBinding = 0;

        public Collection<Binding> bindings = new Collection<Binding>();

        public void Add(IConfigSetting configSetting)
        {
            switch(configSetting.Type)
            {
                case TypeBinding: bindings.Add((Binding)configSetting); break;
            }
        }
    }
}
