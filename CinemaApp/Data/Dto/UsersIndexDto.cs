using System.ComponentModel;

namespace CinemaApp.Data.Dto
{
    public class UsersIndexDto
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Username { get; set; }


    }
}
