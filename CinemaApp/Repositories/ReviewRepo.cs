using CinemaApp.Data;
using CinemaApp.Interfaces;
using CinemaApp.Models;

namespace CinemaApp.Repositories
{
    public class ReviewRepo : IReview
    {

        private readonly ApplicationDbContext _context;
        public ReviewRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Reviews reviews)
        {
            _context.Add(reviews);
            return Save();

        }

        public bool Save()
        {
            var status = _context.SaveChanges();
            if (status > 0)
            {
                return true;
            }
            return false;
        }
    }
}
