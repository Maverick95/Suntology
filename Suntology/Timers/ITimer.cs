using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suntology.Timers
{
    public interface ITimer
    {
        void Run(Action action);
    }
}
