using CinemaApp.Models;

namespace CinemaApp.ViewModels
{
    //We are making this model in order to wrap the Movie we want to Display and also the List of related movies
    public class MovieDetailViewModel
    {
        //The Movie we want to show Details For 
        public Movies MyMovie { get; set; }

        //The Related Movies We want to show 
        public List<Movies> MoreMoviesM { get; set; }
    }
}
