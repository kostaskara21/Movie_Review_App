using System.ComponentModel;
using CinemaApp.Models;

namespace CinemaApp.Data.Dto
{
    public class UsersDetailDto
    {

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Username { get; set; }

        public List<Movies>? Movies { get; set; }
    }
}
