namespace CinemaApp.Data.Dto
{
    public class UserEditDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
