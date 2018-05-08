using System.ComponentModel.DataAnnotations;

namespace VotingApp.Web.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; } // username is same as email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
