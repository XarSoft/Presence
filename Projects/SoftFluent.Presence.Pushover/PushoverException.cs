using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverException : Exception
    {
        public PushoverException(string message) : base(message) { }
        public PushoverException(string message, Exception ex) : base(message, ex) { }
    }
}
