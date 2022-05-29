using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Suntology.Timers
{
    public class StandardTimer: ITimer
    {
        private readonly int _iterations;

        public StandardTimer(int iterations)
        {
            _iterations = iterations;
        }

        public void Run(Action action)
        {
            long totalMs = 0, minMs = 0, maxMs = 0;
            var stopwatch = new Stopwatch();
            
            for (var i = 0; i < _iterations; i++)
            {
                stopwatch.Start();
                action();
                stopwatch.Stop();
                var elapsed = stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();

                totalMs += elapsed;
                if (i is 0 || elapsed < minMs)
                {
                    minMs = elapsed;
                }
                if (i is 0 || elapsed > maxMs)
                {
                    maxMs = elapsed;
                }
            }

            Console.WriteLine("{0} iterations performed", _iterations);
            Console.WriteLine("Fastest time : {0} , Slowest time : {1}, Average time : {2}",
                minMs, maxMs, totalMs / _iterations);
        }
    }
}
