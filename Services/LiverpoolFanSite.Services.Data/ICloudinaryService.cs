namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadFormFileAsync(IFormFile file);

        Task<List<string>> GetListAsync();
    }
}
