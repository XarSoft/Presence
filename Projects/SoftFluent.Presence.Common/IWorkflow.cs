using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftFluent.Presence.Common
{
    public interface IWorkflow
    {
        void Process(EventFromIftttType eventNameChanged, bool value);
    }
}
