using CloudinaryDotNet.Actions;

namespace CinemaApp.Interfaces
{
    public interface IPhotoService
    {

        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string Id);

    }
}
