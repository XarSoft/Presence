using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Host
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            service = new ServiceInstaller();
            service.StartType = ServiceStartMode.Automatic;
            service.ServiceName = GetConfigurationValue("HostName");
            service.DisplayName = GetConfigurationValue("HostName");
            service.Description = GetConfigurationValue("HostName");
            Installers.Add(service);

            process = new ServiceProcessInstaller();
            Installers.Add(process);

            InitializeComponent();
        }

        private static string GetConfigurationValue(string key)
        {
            var service = Assembly.GetAssembly(typeof(ProjectInstaller));
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
            if (config.AppSettings.Settings[key] == null)
            {
                throw new IndexOutOfRangeException("Settings collection does not contain the requested key:" + key);
            }

            return config.AppSettings.Settings[key].Value;
        }

        public string GetContextParameter(string key)
        {
            string sValue = string.Empty;
            try
            {
                sValue = this.Context.Parameters[key].ToString();
            }
            catch
            {
            }

            return sValue;
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            string username = GetContextParameter("user").Trim();
            string password = GetContextParameter("password").Trim();
            string type = GetContextParameter("type").Trim();

            if (type == "LocalSystem")
            {
                process.Account = ServiceAccount.LocalSystem;
            }
            else if (type == "LocalService")
            {
                process.Account = ServiceAccount.LocalService;
            }
            else if (type == "NetworkService")
            {
                process.Account = ServiceAccount.NetworkService;
            }
            else
            {
                process.Account = ServiceAccount.User;
                process.Username = username;
                process.Password = password;
            }
        }
    }
}
