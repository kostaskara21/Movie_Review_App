using CinemaApp.Data.Enum;
using CinemaApp.Models;

namespace CinemaApp.Data.Dto
{
    public class MovieEditDto
    {

        public int Id { get; set; }

        public string Title { get; set; }


        public string Description { get; set; }

       
        public IFormFile Image { get; set; }

        public string? URL { get; set; }

        //Reference to Enum Categories 
        public Categories Categories { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


        //One Movie can have many reviews 
        public List<Reviews?> Reviews { get; set; }
    }
}
