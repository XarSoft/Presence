using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Configuration
{
    public class PushoverTokenConfigurationHandler : ConfigurationSection
    {
        private readonly ConfigurationPropertyCollection _proprietes;
        private readonly ConfigurationProperty _tokens;

        public static string SectionName
        {
            get
            {
                return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            }
        }

        protected static PushoverTokenConfigurationHandler _current;
        public static PushoverTokenConfigurationHandler Current
        {
            get
            {
                if (_current == null)
                {
                    _current = ConfigurationManager.GetSection(SectionName) as PushoverTokenConfigurationHandler;
                    if (_current == null)
                    {
                        throw new ConfigurationErrorsException("Missing '" + SectionName + "' section in " + AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    }
                }

                return _current;
            }
        }

        public PushoverTokenConfigurationHandler()
        {
            _tokens = new ConfigurationProperty("", typeof(PushoverTokenConfigurationElementCollection),
                null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);
            _proprietes = new ConfigurationPropertyCollection();
            _proprietes.Add(_tokens);
        }

        public PushoverTokenConfigurationElementCollection Tokens
        {
            get { return (PushoverTokenConfigurationElementCollection)base[_tokens]; }
        }

        public new PushoverTokenConfigurationElement this[string name]
        {
            get { return Tokens[name]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _proprietes; }
        }
    }
}
