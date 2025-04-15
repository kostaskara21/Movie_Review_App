using CinemaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Data.Dto
{
    public class ReviewsCreateDtos
    {
        public string Comment { get; set; }

        public int MovieId { get; set; }
             
    }
}
