using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Dto
{
    public class AccountLogInDto
    {
        [DisplayName("Email")]
        [Required]
        public string EmailAdress { get; set; }
        
        
        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
