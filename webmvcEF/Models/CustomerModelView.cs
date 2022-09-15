using System.ComponentModel.DataAnnotations;

namespace webmvcEF.Models
{
    public class CustomerModelView
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Please fill it out completely")]
        public string FullName { get; set; }

        [Display(Name = "Gender")]
        public bool? Gender { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please fill it out completely")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please fill it out completely")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please fill it out completely")]
        public string Email { get; set; }
    }
}
