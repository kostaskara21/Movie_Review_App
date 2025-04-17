using CinemaApp.Models;

namespace CinemaApp.Data.Dto
{
    public class DashboardIndexDto
    {
        public List<Movies>? MyMovies { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
