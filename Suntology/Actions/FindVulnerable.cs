using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Suntology.Contexts;

namespace Suntology.Actions
{
    public class FindVulnerable: IAction
    {
        private readonly SuntologyContext _context;

        public FindVulnerable()
        {
            _context = new SuntologyContext();
        }

        public void Run()
        {
            /*
             * Count vulnerable workers by caste (excluding the sun god of course),
             * vulnerable being aged under 16 and Female.
             */

            var groups = _context.Members
                .Include(member => member.Caste)
                .AsNoTracking()
                .Where(member => member.Caste.Reference != "SUN")
                .Where(member => member.Age < 16)
                .Where(member => member.AssignedGender == "F")
                .GroupBy(member => new
                {
                    Id = member.Caste.Id,
                    Reference = member.Caste.Reference
                })
                .Select(castes => new
                {
                    Group = castes.Key,
                    Volume = castes.Count(),
                });

            foreach (var g in groups)
            {
                Console.WriteLine("Group {0} - {1} vulnerable members.", g.Group.Reference, g.Volume);
            }
        }
    }
}
