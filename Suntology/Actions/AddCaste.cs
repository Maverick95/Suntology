using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suntology.Contexts;
using Suntology.Entities;

namespace Suntology.Actions
{
    public class AddCaste: IAction
    {
        private SuntologyContext _context;

        public AddCaste()
        {
            _context = new SuntologyContext();
        }

        public void Run()
        {
            Console.WriteLine("Enter the Reference for your caste : ");
            var reference = Console.ReadLine();
            Console.WriteLine("Enter the Display Name for your caste : ");
            var displayName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(reference) || string.IsNullOrWhiteSpace(displayName))
            {
                Console.WriteLine("Error - Reference and Display Name must be provided.");
                return;
            }

            if (_context.Castes.Any(caste => caste.Reference == reference))
            {
                Console.WriteLine("Error - Reference must be unique. Every caste is unique in the eyes of the glorious sun.");
                return;
            }

            Caste caste = new()
            {
                Reference = reference,
                DisplayName = displayName,
            };

            _context.Castes.Add(caste);
            _context.SaveChanges();
            Console.WriteLine("A new caste of horror has been set unto the world. All rejoice.");
        }

    }
}
