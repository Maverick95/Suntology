using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suntology.Handlers
{
    public interface IHandler
    {
        void Prompt();
        void Handle(string input);
    }
}
