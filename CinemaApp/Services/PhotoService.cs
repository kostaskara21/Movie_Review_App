using System.IO;
using CinemaApp.Helpers;
using CinemaApp.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace CinemaApp.Services
{
    //Services are the same as repos but they abstract services that has not to do with the Db
    public class PhotoService : IPhotoService
    {
        //We are getting this from the Cloudinary Package we install
        private readonly Cloudinary _cloudinary;

        //The Ioption is getting the stuff from the appsettings 
        //With the AddServices from the Program.cs we have these Strings setted(Like key) in the variable of the Class
        //Here we are logging in using the Acc constrauctor and getting attributes from the Cloudinary Class 
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );

            //And we are giving our account to the Cloudinary 
            _cloudinary = new Cloudinary(acc);
        }


        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var UploadResult=new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream=file.OpenReadStream();
                var UploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                UploadResult=await _cloudinary.UploadAsync(UploadParams);
                
            }
            return UploadResult;
        }


        public async Task<DeletionResult> DeletePhotoAsync(string Id)
        {
            var deleteParams = new DeletionParams(Id);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
