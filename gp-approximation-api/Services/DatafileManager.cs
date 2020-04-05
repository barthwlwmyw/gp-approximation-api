using Microsoft.AspNetCore.Http;

namespace gp_approximation_api.Services
{
    public interface IDatafileManager
    {
        string SaveFile(IFormFile file);
    }
    public class DatafileManager : IDatafileManager
    {
        public string SaveFile(IFormFile file)
        {
            //TODO: implementation

            return "filepath";
        }
    }
}
