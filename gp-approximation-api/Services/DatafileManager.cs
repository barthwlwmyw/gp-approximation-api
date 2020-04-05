using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return "filepath";
        }
    }
}
