﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gp_approximation_api.Services;
using System.Text.Json;
using gp_approximation_api.Model;

namespace gp_approximation_api.Controllers
{
    [ApiController]
    public class ApproxTaskController : ControllerBase
    {
        private readonly IDatafileManager _datafileManager;
        private readonly IApproximationTaskManager _approximationTaskManager;

        public ApproxTaskController(
            IDatafileManager datafileManager, 
            IApproximationTaskManager approximationTaskManager)
        {
            _datafileManager = datafileManager;
            _approximationTaskManager = approximationTaskManager;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> Create([FromForm] IFormFile dataFile, [FromForm] string algorithmParams)
        {
            try
            {
                var deserializedParams = JsonSerializer.Deserialize<AlgorithmParams>(algorithmParams);

                var taskGuid = _approximationTaskManager.CreateTask(deserializedParams);
                var datafilePath = await _datafileManager.SaveFile(taskGuid, dataFile);

                _approximationTaskManager.AssignSourceFile(taskGuid, datafilePath);

                _approximationTaskManager.RunTask(taskGuid);

                return Ok(new { taskGuid, progress = 1 });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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