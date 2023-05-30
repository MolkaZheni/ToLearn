using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LearnApp.Models
{
    public class useraccount
    {
        [Key]
        public int UserId { get; set; }
        [Required (ErrorMessage="user name is required")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

    }
}