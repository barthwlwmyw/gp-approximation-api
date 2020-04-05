﻿using System;
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
        private readonly ILogger<ApproxTaskController> _logger;
        private readonly IDatafileManager _datafileManager;
        private readonly IApproximationTaskManager _approximationTaskManager;

        public ApproxTaskController(ILogger<ApproxTaskController> logger, IDatafileManager datafileManager, IApproximationTaskManager approximationTaskManager)
        {
            _logger = logger;
            _datafileManager = datafileManager;
            _approximationTaskManager = approximationTaskManager;
        }

        [HttpPost("api/[controller]")]
        public IActionResult Create()
        {
            FormFile receivedFile = null;

            var datafilePath = _datafileManager.SaveFile(receivedFile);

            var newTaskParams = new TaskParams { DataFilePath = datafilePath };

            var taskGuid = _approximationTaskManager.CreateTask(newTaskParams);

            return Ok(new { taskGuid });
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