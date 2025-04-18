using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Models
{
    public class AppUser : IdentityUser
    {
       
       [DisplayName("First Name")] 
       public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; } 

        public string? ImagUrl {get;set;}
        
       public List<Movies> Movies { get; set; }
       public List<Reviews> Reviews { get; set; }
    }
}
