using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gp_approximation_api.Services;
using Microsoft.Extensions.Logging;

namespace gp_approximation_api.Controllers
{
    [ApiController]
    public class ApproxTaskController : ControllerBase
    {
        private readonly IDatafileManager _datafileManager;
        private readonly IApproximationTaskManager _approximationTaskManager;

        public ApproxTaskController(IDatafileManager datafileManager, IApproximationTaskManager approximationTaskManager)
        {
            _datafileManager = datafileManager;
            _approximationTaskManager = approximationTaskManager;
        }

        [HttpPost("api/[controller]")]
        public IActionResult Create([FromForm] IFormFile file, [FromForm] string algorithmParams)
        {
            IFormFile receivedFile = file;

            var algorithmParamsXXX = algorithmParams;

            var datafilePath = _datafileManager.SaveFile(receivedFile);
            var newTaskParams = new TaskParams { DataFilePath = datafilePath };
            var taskGuid = _approximationTaskManager.CreateTask(newTaskParams);

            Task.Run(() => _approximationTaskManager.RunTask(taskGuid));

            return Ok(new { taskGuid, progress = 1 });
        }

        [HttpGet("api/[controller]/{taskGuid?}")]
        public IActionResult GetTask(Guid taskGuid)
        {
            var approximationTask = _approximationTaskManager.GetTask(taskGuid);

            return Ok(approximationTask);
        }

        [HttpGet("api/[controller]")] 
        public IActionResult GetAll()
        {
            var approximationTasks = _approximationTaskManager.GetTasks();

            return Ok(approximationTasks);
        }
    }

    public class Params
    {
        public int PopulationSize { get; set; }
        public int GenerationsNumber { get; set; }
        public double CrossoverProbability { get; set; }
        public double MutationProbability { get; set; }
    }
}