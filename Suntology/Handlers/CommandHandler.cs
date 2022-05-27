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
        private Dictionary<Command, IAction> _actions;

        public CommandHandler()
        {
            _actions = new()
            {
                { Command.PRINT_EMAIL, new PrintEmail() },
                { Command.PRINT_NAME, new PrintName() },
                { Command.SUNTOLOGY_ADD_CASTE, new AddCaste() },
            };
        }

        public void Handle(string input)
        {
            if (Enum.TryParse(input, out Command command) &&
                _actions.TryGetValue(command, out IAction action))
            {
                action.Run();
            }
            else
            {
                Console.WriteLine("CommandHandler sez... uh-oh! Something went wrong!");
            }
        }
    }
}
