using CinemaApp.Models;

namespace CinemaApp.Interfaces
{
    public interface IReview
    {

        public bool Add(Reviews reviews);
        public bool Save();
    }
}
