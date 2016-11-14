using log4net;
using Newtonsoft.Json;
using SoftFluent.Presence.Common;
using SoftFluent.Presence.Service;
using SoftFluent.Presence.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Host
{
    public partial class Service : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string _saveFileName = "Presence.sav";
        private ServiceHost _hostApplication;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            try
            {
                _log.Debug("Service is starting");

                _hostApplication = new ServiceHost(typeof(PresenceRestService));
                _hostApplication.Open();

                _log.DebugFormat("ServiceHost at '{0}' is listenning", ConfigurationManager.AppSettings["LocalUrl"]);

                try
                {
                    EventDictionary.Current.LoadDictionnary(JsonConvert.DeserializeObject<EventDictionary>(File.ReadAllText(_saveFileName)));
                    File.Delete(_saveFileName);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }

                Thread thread = new Thread(new ThreadStart(delegate()
                {
                    WorkflowManager.Process();
                }));

                thread.IsBackground = true;
                thread.Start();

                _log.Debug("Service is started");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                WorkflowManager.Stop();

                string json = JsonConvert.SerializeObject(EventDictionary.Current);
                File.WriteAllText(_saveFileName, json);

                _hostApplication.Close();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
