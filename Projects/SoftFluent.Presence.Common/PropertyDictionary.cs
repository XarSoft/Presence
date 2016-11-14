using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Common
{
    public class EventDictionary : Dictionary<EventFromIftttType, bool>
    {
        private static EventDictionary _current;
        public static EventDictionary Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new EventDictionary();
                }

                return _current;
            }
        }

        public EventDictionary()
        {
        }

        public void LoadDictionnary(EventDictionary dict)
        {
            foreach (var val in dict)
            {
                if (!this.ContainsKey(val.Key))
                {
                    this.Add(val.Key, val.Value);
                }
                else
                {
                    this[val.Key] = val.Value;
                }
            }
        }

        public new bool this[EventFromIftttType key]
        {
            get
            {
                if (!this.ContainsKey(key))
                    return false;
                else
                    return base[key];
            }
            set
            {
                if (!this.ContainsKey(key))
                    this.Add(key, false);

                base[key] = value;
            }
        }
    }
}
