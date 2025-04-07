using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaApp.Data.Enum;

namespace CinemaApp.Models
{
    public class Movies
    {
       //Definning the Primary Key 
       [Key]
       public int Id { get; set; }

       public string Title { get; set; }

       [MaxLength(320)]
       public string Description { get; set; }

       public string Image { get; set; }

       //Reference to Enum Categories 
       public Categories Categories { get; set; }


       [ForeignKey("AppUser")]
       public string? AppUserId { get; set; }
       public AppUser? AppUser { get; set; }


       //One Movie can have many reviews 
       public List<Reviews?> Reviews { get; set; }
    }
}
