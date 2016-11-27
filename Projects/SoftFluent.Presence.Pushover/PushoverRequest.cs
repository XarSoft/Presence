using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Pushover
{
    public class PushoverRequest
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public PushoverPriorityNotificationType? Priority { get; private set; }
        public PushoverSoundType? Sound { get; private set; }
        public string[] Devices { get; private set; }
        public string Url { get; private set; }
        public string UrlTitle { get; private set; }
        public int? Retry { get; private set; }
        public int? Expire { get; private set; }
        
        public DateTime? TimeStamp { get; set; }
        public bool? UseHtml { get; set; }
        public string CallbackUrl { get; set; }
        private PushoverRequest()
        {
            UseHtml = true;
        }

        public static PushoverRequest CreateRequest(string message, string title = null, params string[] devices)
        {
            return CreateRequest(message, title, null, null, null, null, null, null, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, PushoverPriorityNotificationType priority, params string[] devices)
        {
            return CreateRequest(message, title, null, null, null, null, priority, null, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, string url, string urlTitle, PushoverPriorityNotificationType priority, params string[] devices)
        {
            return CreateRequest(message, title, url, urlTitle, null, null, priority, null, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, int retry, int expire, params string[] devices)
        {
            return CreateRequest(message, title, null, null, retry, expire, PushoverPriorityNotificationType.EmergencyPriority, null, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, string url, string urlTitle, int retry, int expire, params string[] devices)
        {
            return CreateRequest(message, title, url, urlTitle, retry, expire, PushoverPriorityNotificationType.EmergencyPriority, null, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, PushoverPriorityNotificationType priority, PushoverSoundType? sound, params string[] devices)
        {
            return CreateRequest(message, title, null, null, null, null, priority, sound, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, string url, string urlTitle, PushoverPriorityNotificationType priority, PushoverSoundType? sound, params string[] devices)
        {
            return CreateRequest(message, title, url, urlTitle, null, null, priority, sound, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, int retry, int expire, PushoverSoundType? sound, params string[] devices)
        {
            return CreateRequest(message, title, null, null, retry, expire, PushoverPriorityNotificationType.EmergencyPriority, sound, devices);
        }

        public static PushoverRequest CreateRequest(string message, string title, string url, string urlTitle, int? retry, int? expire, PushoverPriorityNotificationType? priority, PushoverSoundType? sound, params string[] devices)
        {
            if (string.IsNullOrEmpty(message))
                throw new PushoverException("Message cannot be empty");

            if (expire != null && retry == null || expire == null && retry != null)
                throw new PushoverException("Expire and Retry needs to be filled every 2");

            if (expire != null && retry != null && priority != PushoverPriorityNotificationType.EmergencyPriority)
                throw new PushoverException("When Expire and Retry is informed then priority should be set to Emergency priority");

            if ((expire == null || retry == null) && priority == PushoverPriorityNotificationType.EmergencyPriority)
                throw new PushoverException("When Priority is set to Emergency Expire and Retry should be informed too");

            if (retry != null && retry < 30)
                throw new PushoverException("Retry must have a value of at least 30 seconds");

            if (expire != null && expire > 86400)
                throw new PushoverException("Expire must have a maximum value of at most 86400 seconds (24 hours)");

            if (urlTitle != null && url == null)
                throw new PushoverException("When UrlTitle is informed then Url should be informed too");

            PushoverRequest request = new PushoverRequest()
            {
                Devices = devices,
                Expire = expire,
                Message = message,
                Priority = priority,
                Retry = retry,
                Sound = sound,
                Title = title,
                Url = url,
                UrlTitle = urlTitle
            };

            return request;
        }
    }
}
