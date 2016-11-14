using log4net;
using SoftFluent.Presence.Common;
using SoftFluent.Presence.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftFluent.Presence.Service
{
    public class PresenceRestService : IPresenceRestService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void SetProperty(string propertyName)
        {
            try
            {
                _log.DebugFormat("{0} is set to true", propertyName);

                EventFromIftttType value = EnumHelper.GetEventNameEnum(propertyName);
                if (value == EventFromIftttType.Unknown)
                {
                    _log.DebugFormat("Unknow type '{0}' received in SetProperty");
                    return;
                }

                lock (EventDictionary.Current)
                {
                    if (EventDictionary.Current[value] != true)
                    {
                        EventDictionary.Current[value] = true;
                        WorkflowManager.NewEventReceived(value, true);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        public void RemoveProperty(string propertyName)
        {
            try
            {
                 _log.DebugFormat("{0} is set to false", propertyName);

                EventFromIftttType value = EnumHelper.GetEventNameEnum(propertyName);
                if (value == EventFromIftttType.Unknown)
                {
                    _log.DebugFormat("Unknow type '{0}' received in RemoveProperty");
                    return;
                }

                lock (EventDictionary.Current)
                {
                    if (EventDictionary.Current[value] != false)
                    {
                        EventDictionary.Current[value] = false;
                        WorkflowManager.NewEventReceived(value, false);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}