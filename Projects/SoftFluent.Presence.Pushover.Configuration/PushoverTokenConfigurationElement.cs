using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Configuration
{
    public class PushoverTokenConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)GetValue("name");
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)GetValue("value");
            }
        }

        protected object GetValue(string key)
        {
            return this[key];
        }
    }
}
