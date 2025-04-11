using CinemaApp.Models;

namespace CinemaApp.Data.Dto
{
    //We are making this model in order to wrap the Movie we want to Display and also the List of related movies
    public class MovieDetailDto
    {
        //The Movie we want to show Details For 
        public Movies MyMovie { get; set; }

       //Added this in order to get the reviews from the detailed model 
        public List<Reviews> Reviews { get; set; }

        //The Related Movies We want to show 
        public List<Movies> MoreMoviesM { get; set; }
    }
}
