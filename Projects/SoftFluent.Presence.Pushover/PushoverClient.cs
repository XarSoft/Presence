using log4net;
using Newtonsoft.Json;
using SoftFluent.Presence.Common;
using SoftFluent.Presence.Pushover.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverClient
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int Timeout
        {
            get
            {
                return PushoverConfigurationHandler.Current.Timeout;
            }
        }

        public string Token
        {
            get
            {
                return PushoverTokenConfigurationHandler.Current.Tokens["presence"].Value;
            }
        }

        public string UserKey
        {
            get
            {
                return PushoverConfigurationHandler.Current.UserKey;
            }
        }

        public string Url
        {
            get
            {
                return PushoverConfigurationHandler.Current.Url;
            }
        }

        private WebProxy _proxy;
        public WebProxy Proxy
        {
            get
            {
                if (_proxy == null)
                {
                    if (PushoverProxyConfigurationHandler.Current.IsEnabled)
                    {
                        _proxy = new WebProxy(PushoverProxyConfigurationHandler.Current.Host, PushoverProxyConfigurationHandler.Current.Port);
                    }
                }

                return _proxy;
            }
        }

        public PushoverResponse SendNotification(string message)
        { 
            return SendNotification(PushoverRequest.CreateRequest(message));
        }

        public PushoverResponse SendNotification(PushoverRequest pushoverRequest)
        {
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(Url, "messages"));
                request.Timeout = Timeout;
                request.Proxy = Proxy;
                request.Method = "POST";

                string postData = "token=" + Token;
                postData += "&user=" + UserKey;
                postData += "&message=" + WebUtility.UrlEncode(pushoverRequest.Message);

                if (!string.IsNullOrEmpty(pushoverRequest.Title))
                {
                    postData += "&title=" + WebUtility.UrlEncode(pushoverRequest.Title);
                }
                if (pushoverRequest.Priority.HasValue)
                {
                    postData += "&priority=" + (int)pushoverRequest.Priority.Value;
                }
                if (pushoverRequest.UseHtml.HasValue)
                {
                    postData += "&html=" + (pushoverRequest.UseHtml.Value ? "1" : "0");
                }
                if (pushoverRequest.Sound.HasValue)
                {
                    postData += "&sound=" + pushoverRequest.Sound.Value.ToString();
                }
                if (pushoverRequest.Devices != null)
                {
                    postData += "&device=" + WebUtility.UrlEncode(string.Join(",", pushoverRequest.Devices));
                }
                if (pushoverRequest.Url != null)
                {
                    postData += "&url=" + pushoverRequest.Url;
                }
                if (pushoverRequest.UrlTitle != null)
                {
                    postData += "&url_title=" + WebUtility.UrlEncode(pushoverRequest.UrlTitle);
                }
                if (pushoverRequest.Expire.HasValue)
                {
                    postData += "&expire=" + pushoverRequest.Expire.Value;
                }
                if (pushoverRequest.Retry.HasValue)
                {
                    postData += "&retry=" + pushoverRequest.Retry.Value;
                }
                if (pushoverRequest.TimeStamp.HasValue)
                {
                    postData += "&timestamp=" + DateTimeHelper.DateTimeToUnixTimestamp(pushoverRequest.TimeStamp.Value);
                }
                if (pushoverRequest.CallbackUrl != null)
                {
                    postData += "&callback=" + WebUtility.UrlEncode(pushoverRequest.CallbackUrl);
                }

                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = (long)data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<PushoverResponse>(responseString);
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                _log.ErrorFormat("Error {0} ({1}) received : {2}", (int)response.StatusCode, response.StatusCode, responseString);

                return JsonConvert.DeserializeObject<PushoverResponse>(responseString);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return null;
        }

        public PushoverEmergencyResponse GetEmergencyStatus(string receiptId)
        {
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(Url, "receipts/" + receiptId) + "?token=" + Token);
                request.Timeout = Timeout;
                request.Proxy = Proxy;
                request.Method = "GET";

                response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<PushoverEmergencyResponse>(responseString);
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;

                _log.ErrorFormat("Error {0} ({1}) received", (int)response.StatusCode, response.StatusCode);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return null;
        }

        public PushoverCancelEmergencyResponse CancelEmergency(string receiptId)
        {
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(Url, "receipts/" + receiptId + "/cancel"));
                request.Timeout = Timeout;
                request.Proxy = Proxy;
                request.Method = "POST";

                string postData = "token=" + Token;
                
                byte[] data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = (long)data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<PushoverCancelEmergencyResponse>(responseString);
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;

                _log.ErrorFormat("Error {0} ({1}) received", (int)response.StatusCode, response.StatusCode);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return null;
        }
    }
}
