using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "AppUserId", "Categories", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 1, null, 0, "Harry Potter learns he's a wizard and attends Hogwarts. He faces many challenges, including discovering the truth about his parents' death and Voldemort's return.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS1jr5jmHTMu5Hv04GjrZG5EaaD5zYshberRA&s", "Harry Potter and the Sorcerer's Stone" },
                    { 2, null, 0, "In a dystopian future, hacker Neo learns that reality as he knows it is a simulated construct created by machines to subdue the human population. With the help of a rebel group, he begins to fight back against the machines and discovers his true potential.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQQxjuaNUQXt5TOG8HPVClcQMJ76r6aHlGqzw&s", "The Matrix" },
                    { 3, null, 0, "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP3-1_Sx_WFXRqb2NpigiDkggpQ5ovXavR9w&s", "The Dark Knight" },
                    { 4, null, 3, "During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run wild. A group of people must escape from the island alive as prehistoric creatures hunt them down.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfQ9vdwTdbTw4SupATzd7Z2jqW9iCRfrXxag&s", "Jurassic Park" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
