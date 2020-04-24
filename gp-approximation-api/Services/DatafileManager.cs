using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace gp_approximation_api.Services
{
    public interface IDatafileManager
    {
        Task<string> SaveFile(IFormFile file);
    }
    public class DatafileManager : IDatafileManager
    {
        public async Task<string> SaveFile(IFormFile file)
        {
            using (var stream = File.Create("myFile.txt"))
            {
                await file.CopyToAsync(stream);
            }

            return "myFile.txt";
        }
    }
}
