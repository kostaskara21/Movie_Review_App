using System.ComponentModel;

namespace CinemaApp.Data.Dto
{
    public class UsersIndexDto
    {
        public string? id { get;set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Username { get; set; }

        public string? Img { get; set; }


    }
}
