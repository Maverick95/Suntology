using Suntology.Handlers;
using System;

namespace Suntology
{
    public class Program
    {
        static void Main(string[] args)
        {
            IHandler handler = new CommandHandler();
            const string escape = "Get me out of here!";
            string input;
            while ((input = Console.ReadLine()) != escape)
            {
                handler.Handle(input);
            }
        }
    }
}
