using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class AppUser
    {
       [Key]
       public string Id { get; set; }

       public List<Movies> Movies { get; set; }
       public List<Reviews> Reviews { get; set; }
    }
}
