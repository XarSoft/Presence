using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            System.ServiceModel.ServiceHost _hostApplication = new System.ServiceModel.ServiceHost(typeof(SoftFluent.Presence.Service.PresenceRestService));
            _hostApplication.Open();

            PushoverClient client = new PushoverClient();
            client.SendNotification("first message");

            PushoverRequest request = PushoverRequest.CreateRequest("Test", "Presence", PushoverPriorityNotificationType.LowPriority, "Tel_Softfluent");

            //PushoverRequest request = PushoverRequest.CreateRequest("Test", null, 30, 120, PushoverPriorityNotificationType.EmergencyPriority, "Tel_Softfluent");
            //request.TimeStamp = new DateTime(2016, 1, 31, 10, 5, 0);

            PushoverResponse response = client.SendNotification(request);
            if (response != null && response.Status == 1)
            {
                //OK
                client.CancelEmergency("");

            }
        }
    }
}
