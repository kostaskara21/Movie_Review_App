using CinemaApp.Models;
using CinemaApp.ViewModels;

namespace CinemaApp.Interfaces
{
    public interface IMovies
    {

        //With this we are returning all the Movies
        Task<List<Movies>> Index();

        //We are returning a MovieDetailViewModel because we want to return 2  thingd(List and Specific Movie)
        //We are making the object in this function and returning it in the controller
        Task<MovieDetailViewModel> Details(int id);


        Task<Movies> Edit(int id);


        //For Details Page we need to do 2 things:
        // 1)pass specific Movie,
        // 2) pass List of related Movies
        // So these 2 things can split in 2 diferrent functions with diff logic
        // for making the code cleaner and better for testing and mantaining 
        Task<Movies> GetMovieById(int id);
        Task<List<Movies>> GetRelatedMovies(int id);


        bool Add(Movies movie);
        bool Remove(Movies movie);
        bool Update(int Id);
        bool Save();





    }
}
