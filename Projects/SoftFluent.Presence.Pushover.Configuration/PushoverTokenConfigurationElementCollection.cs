using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Configuration
{
    public class PushoverTokenConfigurationElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "token"; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return new ConfigurationPropertyCollection(); }
        }

        public PushoverTokenConfigurationElement this[int index]
        {
            get { return (PushoverTokenConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        public new PushoverTokenConfigurationElement this[string nom]
        {
            get { return (PushoverTokenConfigurationElement)BaseGet(nom); }
        }

        public void Add(PushoverTokenConfigurationElement item)
        {
            base.BaseAdd(item);
        }

        public void Remove(PushoverTokenConfigurationElement item)
        {
            BaseRemove(item);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PushoverTokenConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element != null)
                return ((PushoverTokenConfigurationElement)element).Name;
            else
                return null;
        }
    }
}
