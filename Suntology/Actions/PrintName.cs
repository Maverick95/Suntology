using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suntology.Actions
{
    public class PrintName: IAction
    {
        public void Run()
        {
            Console.WriteLine("Nick Emmerson");
        }
    }
}
