using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Models
{
    public class Reviews
    {

        public int Id { get; set; }

        public string Comment { get; set; }

       

        //Our Foreign Key for Movies table
        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies Movies { get; set; }


        //Our Foreign Key for AppUser table
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        
        public  AppUser AppUser { get; set; }


    }
}
