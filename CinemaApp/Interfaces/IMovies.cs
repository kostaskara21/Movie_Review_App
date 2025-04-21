using CinemaApp.Data.Dto;
using CinemaApp.Helpers;
using CinemaApp.Models;


namespace CinemaApp.Interfaces
{
    public interface IMovies
    {

        //With this we are returning all the Movies
        Task<List<Movies>> Index();
        Task<(List<Movies> movies,Pager p)> Index(int pg);

       


        //For Details Page we need to do 2 things:
        //We are returning a MovieDetailViewModel because we want to return 2  thingd(List and Specific Movie)
        // 1)pass specific Movie,
        // 2) pass List of related Movie
        Task<Movies> GetMovieById(int id);
        Task<Movies> GetMovieByIdNoTrack(int id);
        Task<List<Movies>> GetRelatedMovies(int id);


        bool Add(Movies movie);
        bool Remove(Movies movie);
        bool Update(Movies movie);
        bool Save();





    }
}
