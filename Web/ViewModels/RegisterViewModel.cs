using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords arent matching!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }


        [Required(ErrorMessage = "• Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "• Please enter surname")]
        public string Surname { get; set; }

        [Phone]
        [Required(ErrorMessage = "• Please enter phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "• Please enter position")]
        public Position Position { get; set; }

        public string PhotoURL { get; set; }
    }
}
