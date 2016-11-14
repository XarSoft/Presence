using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Configuration
{
    public class PushoverProxyConfigurationHandler : ConfigurationSection
    {
        public static string SectionName
        {
            get
            {
                return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            }
        }

        protected static PushoverProxyConfigurationHandler _current;
        public static PushoverProxyConfigurationHandler Current
        {
            get
            {
                if (_current == null)
                {
                    _current = ConfigurationManager.GetSection(SectionName) as PushoverProxyConfigurationHandler;
                    if (_current == null)
                    {
                        _current = new PushoverProxyConfigurationHandler();
                    }
                }

                return _current;
            }
        }

        [ConfigurationProperty("host", IsRequired = false, DefaultValue = "")]
        public string Host
        {
            get
            {
                return (string)GetValue("host");
            }
        }

        [ConfigurationProperty("port", IsRequired = false, DefaultValue = 0)]
        public int Port
        {
            get
            {
                return (int)GetValue("port");
            }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = false)]
        public bool IsEnabled
        {
            get
            {
                return (bool)GetValue("enabled");
            }
        }

        protected object GetValue(string key)
        {
            return this[key];
        }
    }
}
