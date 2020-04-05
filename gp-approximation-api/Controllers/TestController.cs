using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging;

namespace gp_approximation_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        delegate void MyCallback(int x);

        [DllImport(@"gp-approximation-engine.dll", EntryPoint = "callback_test")]
        static extern void TestCallback([MarshalAs(UnmanagedType.FunctionPtr)]MyCallback func);

        [HttpGet]
        public IActionResult Healthcheck()
        {
            Task.Run(() => TestCallback((x) => Console.WriteLine(x)));
            return Ok();
        }

        [HttpPost]
        public IActionResult Upload([FromForm] List<IFormFile> file)
        {
            var file2 = file.First();

            var varFileName = file2.FileName;

            try
            {
                SaveFile(file2);
            }
            finally
            { }

            return Ok(varFileName + DoStf(2,3).ToString());
        }

        private static void SaveFile(IFormFile dataFile)
        {
            using FileStream DestinationStream = System.IO.File.Create($"{Guid.NewGuid()}_{dataFile.FileName}");
            dataFile.CopyTo(DestinationStream);
        }

        [DllImport(@"gp-approximation-engine.dll", EntryPoint = "do_stuff")]
        private static extern int DoStf(int a, int b);

    }

    public class ApproximationService
    {
        public static void DoWork(string passSth)
        {
            for(int i = 0; i< 10; i++)
            {
                Thread.Sleep(240);
                Console.WriteLine($"DOIN STUFF {i}/10, passed: {passSth}");
            }
        }
    }
}