using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gp_approximation_api.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using gp_approximation_api.Model;

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
        public async Task<IActionResult> Create([FromForm] IFormFile file, [FromForm] string algorithmParams)
        {
            IFormFile receivedFile = file;

            var deserializedParams = JsonSerializer.Deserialize<AlgorithmParams>(algorithmParams);

            var datafilePath = await _datafileManager.SaveFile(receivedFile);
            var taskGuid =  _approximationTaskManager.CreateTask(datafilePath);

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
}