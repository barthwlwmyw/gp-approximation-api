using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace gp_approximation_api.Services
{
    public interface IDatafileManager
    {
        Task<string> SaveFile(Guid taskGuid, IFormFile file);
    }
    public class DatafileManager : IDatafileManager
    {
        public async Task<string> SaveFile(Guid taskGuid, IFormFile file)
        {
            var filePath = $"Temps/{taskGuid}_data.txt";

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
