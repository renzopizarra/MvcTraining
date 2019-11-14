using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(225)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(225)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(225)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(225)]
        public string Address { get; set; }
    }
}
