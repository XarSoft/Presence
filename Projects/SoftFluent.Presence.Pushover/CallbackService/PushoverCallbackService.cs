using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverCallbackService : IPushoverCallbackService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Callback(Stream streamdata)
        {
            try
            {
                StreamReader reader = new StreamReader(streamdata);
                string res = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                PushoverCallbackRequest callbackRequest = JsonConvert.DeserializeObject<PushoverCallbackRequest>(res);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}