using log4net;
using SoftFluent.Presence.Common;
using SoftFluent.Presence.Pushover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Workflows
{
    public class WorkPresenceWorkflow : IWorkflow
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static WorkPresenceWorkflow _current;
        public static WorkPresenceWorkflow Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new WorkPresenceWorkflow();
                }

                return _current;
            }
        }

        private WorkPresenceWorkflow()
        {
        }

        public void Process(EventFromIftttType eventNameChanged, bool newValue)
        {
            _log.DebugFormat("Event '{0}' is processing in '{1}'", eventNameChanged, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            PushoverClient client = new PushoverClient();
            if (newValue)
            {
                client.SendNotification(PushoverRequest.CreateRequest("Sotfluent arrive au travail", "Presence", PushoverPriorityNotificationType.NormalPriority, PushoverSoundType.pushover));
            }
            else
            {
                client.SendNotification(PushoverRequest.CreateRequest("Sotfluent part du travail", "Presence", PushoverPriorityNotificationType.NormalPriority, PushoverSoundType.pushover));
            }
        }
    }
}
