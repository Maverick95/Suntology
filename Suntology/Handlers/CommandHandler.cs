using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suntology.Constants;
using Suntology.Actions;
using Suntology.Timers;
using System.Text.RegularExpressions;

namespace Suntology.Handlers
{
    public class CommandHandler: IHandler
    {
        private class CommandDetails
        {
            public string Description { get; set; }
            public Func<IAction> Action { get; set; }
        }

        private class TimerDetails
        {
            public string Description { get; set; }
            public Func<ITimer> Factory { get; set; }
        }

        private Dictionary<Command, CommandDetails> _actions;

        private Dictionary<Timer, TimerDetails> _timers;

        private ITimer? _timer;

        public CommandHandler()
        {
            _actions = new()
            {
                {
                    Command.SUNTOLOGY_ADD_CASTE,
                    new()
                    {
                        Description = "Add a new caste",
                        Action = () => new AddCaste(),
                    }
                },
                {
                    Command.SUNTOLOGY_FIND_VULNERABLE,
                    new()
                    {
                        Description = "Find vulnerable workers",
                        Action = () => new FindVulnerable(),
                    }
                }
            };

            _timers = new()
            {
                {
                    Timer.TIMER_STANDARD_10,
                    new()
                    {
                        Description = "Run action 10 times",
                        Factory = () => new StandardTimer(10),
                    }
                }
            };
        }

        public void Prompt()
        {
            foreach(var a in _actions)
            {
                Console.WriteLine("{0}\t{1}", a.Key, a.Value.Description);
            }
        }

        public void Handle(string input)
        {
            if ((new Regex("^RUN .+")).IsMatch(input))
            {
                var run_input = input.Substring(4);

                if (Enum.TryParse(run_input, out Command command) &&
                    _actions.TryGetValue(command, out CommandDetails details))
                {
                    if (_timer is not null)
                    {
                        _timer.Run(() => details.Action().Run());
                    }
                    else
                    {
                        details.Action().Run();
                    }
                }
                else
                {
                    Console.WriteLine("CommandHandler sez... uh-oh! Something went wrong!");
                }
            }
            else if ((new Regex("^TIMER .+")).IsMatch(input))
            {
                var timer_input = input.Substring(6);

                if (timer_input is "NONE")
                {
                    _timer = null;
                }
                else if (Enum.TryParse(timer_input, out Timer timer) &&
                    _timers.TryGetValue(timer, out TimerDetails details))
                {
                    _timer = details.Factory();
                }
                else
                {
                    Console.WriteLine("CommandHandler sez... uh-oh! Something went wrong!");
                }
            }
            else
            {
                Console.WriteLine("CommandHandler sez... uh-oh! Something went wrong!");
            }
        }
    }
}
