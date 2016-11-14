using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Configuration
{
    public class PushoverConfigurationHandler : ConfigurationSection
    {
        public static string SectionName
        {
            get
            {
                return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            }
        }

        protected static PushoverConfigurationHandler _current;
        public static PushoverConfigurationHandler Current
        {
            get
            {
                if (_current == null)
                {
                    _current = ConfigurationManager.GetSection(SectionName) as PushoverConfigurationHandler;
                    if (_current == null)
                    {
                        _current = new PushoverConfigurationHandler();
                    }
                }

                return _current;
            }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)GetValue("url");
            }
        }

        [ConfigurationProperty("userkey", IsRequired = true)]
        public string UserKey
        {
            get
            {
                return (string)GetValue("userkey");
            }
        }

        [ConfigurationProperty("timeout", IsRequired = false, DefaultValue=30 * 1000)]
        public int Timeout
        {
            get
            {
                return (int)GetValue("timeout");
            }
        }

        protected object GetValue(string key)
        {
            return this[key];
        }
    }
}
