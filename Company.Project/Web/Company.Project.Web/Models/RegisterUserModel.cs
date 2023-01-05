
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Project.Web.Models
{
    //Register user model for getting and setting register user properties 
    /// <summary>
    /// Getting of the information from Register View()
    /// </summary>
    public class RegisterUserModel
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",ErrorMessage ="Name is not Valid")]
        public string UserName { get; set; }



        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter a valid email address")]
        public string EmailId { get; set; }



        [Required]
        [MinLength(5,ErrorMessage = "Minimum length should be 5")]
        public string Password { get; set; }



        [NotMapped] // Does not effect with database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
