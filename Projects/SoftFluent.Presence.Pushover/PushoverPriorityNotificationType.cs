using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public enum PushoverPriorityNotificationType
    {
        LowestPriority = -2,
        LowPriority = -1,
        NormalPriority = 0,
        HighPriority = 1,
        EmergencyPriority = 2
    }
}
