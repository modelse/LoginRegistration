using System.ComponentModel.DataAnnotations;
using System;

namespace loginReg.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [MinLength(2)]
        public string first {get; set;}

        [Required]
        [Display(Name = "Last Name:")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [MinLength(2)]
        public string last {get; set;}

        [Required]
        [Display(Name = "Email Address:")]
        [EmailAddress]
        public string email {get; set;}

        [Required]
        [Display(Name = "Password:")]
        [MinLength(8)]
        public string password{get; set;}

        [Required]
        [Display(Name = "Confirm Password:")]
        [Compare("password", ErrorMessage = "Password confirmation must match Password")]
        public string passwordCon{get; set;}

        public DateTime created_at{get; set;}


        public DateTime updated_at{get; set;}
    }
}