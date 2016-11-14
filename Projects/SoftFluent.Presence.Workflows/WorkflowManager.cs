using log4net;
using SoftFluent.Presence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Workflows
{
    public class WorkflowManager
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Queue<Tuple<EventFromIftttType, bool>> _queue = new Queue<Tuple<EventFromIftttType, bool>>();
        private static object _lockQueue = new object();
        private static ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
        private static bool _isStopping = false;

        public static void NewEventReceived(EventFromIftttType eventName, bool value)
        {
            lock (_lockQueue)
            {
                _queue.Enqueue(new Tuple<EventFromIftttType, bool>(eventName, value));
                _manualResetEvent.Set();
            }
        }

        public static void Stop()
        {
            lock (_lockQueue)
            {
                _isStopping = true;
                _manualResetEvent.Set();
            }
        }

        public static void Process()
        {
            try
            {
                while (!_isStopping)
                {
                    bool hasElement = false;
                    Tuple<EventFromIftttType, bool> eventReceived;

                    lock (_lockQueue)
                    {
                        hasElement = _queue.Count > 0;
                    }

                    if (!hasElement)
                    {
                        _manualResetEvent.WaitOne();
                    }

                    if (_isStopping)
                        break;

                    lock (_lockQueue)
                    {
                        eventReceived = _queue.Dequeue();
                        _manualResetEvent.Reset();
                    }

                    try
                    {
                        WorkPresenceWorkflow.Current.Process(eventReceived.Item1, eventReceived.Item2);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
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
