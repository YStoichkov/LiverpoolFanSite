namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<List<string>> GetListAsync()
        {
            var listResult = new List<string>();
            var all = await this.cloudinary.ListResourcesAsync();
            if (all.StatusCode == HttpStatusCode.OK)
            {
                foreach (var resourse in all.Resources)
                {
                    listResult.Add(resourse.SecureUrl.AbsoluteUri);
                }
            }

            return listResult;
        }

        public async Task<string> UploadFormFileAsync(IFormFile file)
        {
            byte[] destinationImage;

            using (var memory = new MemoryStream())
            {
                await file.CopyToAsync(memory);
                destinationImage = memory.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };

                var res = await this.cloudinary.UploadAsync(uploadParams);

                return res.Url.AbsoluteUri;
            }
        }
    }
}
