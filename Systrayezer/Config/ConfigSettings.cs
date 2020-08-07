using System.Collections.ObjectModel;
using Systrayezer.Config;
using System.Linq;

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

        public void applyBindings(KeyboardHook hook)
        {
            Collection<Config.Binding> bindings = UserConfig.config.bindings;

            for (int idxBinding = 0; idxBinding < bindings.Count; idxBinding++)
            {
                Config.Binding binding = bindings.ElementAt(idxBinding);
                if (binding.eventKeyId == 0)
                {
                    binding.apply(hook);
                }
            }
        }
    }
}
