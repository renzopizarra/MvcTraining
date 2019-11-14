using System.ComponentModel.DataAnnotations;

namespace DemoApp.WebMvc.Models
{
    public class ModelEmployeeMvc
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
