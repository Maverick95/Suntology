using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suntology.Commands;
using Suntology.Actions;

namespace Suntology.Handlers
{
    public class CommandHandler: IHandler
    {
        private class Details
        {
            public string Description { get; set; }
            public IAction Action { get; set; }
        }

        private Dictionary<Command, Details> _actions;

        public CommandHandler()
        {
            _actions = new()
            {
                {
                    Command.SUNTOLOGY_ADD_CASTE,
                    new Details
                    {
                        Description = "Add a new caste",
                        Action = new AddCaste()
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
            if (Enum.TryParse(input, out Command command) &&
                _actions.TryGetValue(command, out Details details))
            {
                details.Action?.Run();
            }
            else
            {
                Console.WriteLine("CommandHandler sez... uh-oh! Something went wrong!");
            }
        }
    }
}
