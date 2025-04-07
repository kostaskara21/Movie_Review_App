using CinemaApp.Data.Enum;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Movies>().HasData(
                 new Movies
                 {
                     Id = 1,
                     Title = "Harry Potter and the Sorcerer's Stone",
                     Description = "Harry Potter learns he's a wizard and attends Hogwarts. He faces many challenges, including discovering the truth about his parents' death and Voldemort's return.",

                     Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1jr5jmHTMu5Hv04GjrZG5EaaD5zYshberRA&s",
                     Categories = Categories.Action,
                     AppUserId = null
                 },
                 new Movies
                 {
                     Id = 2,
                     Title = "The Matrix",
                     Description = "In a dystopian future, hacker Neo learns that reality as he knows it is a simulated construct created by machines to subdue the human population. With the help of a rebel group, he begins to fight back against the machines and discovers his true potential.",

                     Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQQxjuaNUQXt5TOG8HPVClcQMJ76r6aHlGqzw&s",
                     Categories = Categories.Action,
                     AppUserId = null
                 },
                 new Movies
                 {
                     Id = 3,
                     Title = "The Dark Knight",
                     Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",

                     Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP3-1_Sx_WFXRqb2NpigiDkggpQ5ovXavR9w&s",
                     Categories = Categories.Action,
                     AppUserId = null
                 },
                 new Movies
                 {
                     Id = 4,
                     Title = "Jurassic Park",
                     Description = "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run wild. A group of people must escape from the island alive as prehistoric creatures hunt them down.",

                     Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfQ9vdwTdbTw4SupATzd7Z2jqW9iCRfrXxag&s",
                     Categories = Categories.Fantasy,
                     AppUserId = null
                 });
            
        }
    }
}
