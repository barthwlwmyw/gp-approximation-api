using System;
using gp_approximation_api.Model;
using gp_approximation_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gp_approximation_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly GpApproximationManager _gpApproximationManager;

        public SessionController(GpApproximationManager gpApproximationManager)
        {
            _gpApproximationManager = gpApproximationManager;
        }

        [HttpGet]
        public IActionResult GetGuid()
        {
            var guid = Guid.NewGuid();

            GpApproximationManager.ApproximationTasks.Add(new ApproximationTask { TaskGuid = guid, TaskProgress = 0 });

            return Ok(new { SessionGuid = guid });
        }
        [HttpGet]
        [Route("/all")]
        public IActionResult GetAllTasks()
        {
            return Ok(new { Tasks = GpApproximationManager.ApproximationTasks });
        }
    }
}