using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Common
{
    public class EnumHelper
    {
        public static EventFromIftttType GetEventNameEnum(string enumString)
        {
            EventFromIftttType enumValue = EventFromIftttType.Unknown;
            if (!System.Enum.TryParse(enumString, out enumValue))
                return EventFromIftttType.Unknown;

            return enumValue;
        }
    }
}
