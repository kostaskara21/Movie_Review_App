using CinemaApp.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Dto
{
    public class MovieCreateDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        //Reference to Enum Categories 
        public Categories Categories { get; set; }  
    }
}
