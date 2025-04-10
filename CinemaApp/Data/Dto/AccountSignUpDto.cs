using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CinemaApp.Data.Dto
{
    public class AccountSignUpDto
    {

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }


        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }


        [DisplayName("Email")]
        [Required]
        public string EmailAdress { get; set; }


        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
