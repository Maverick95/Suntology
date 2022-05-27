using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suntology.Entities
{
    [Table("suntology.caste")]
    public class Caste
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reference { get; set; }

        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }
    }
}
