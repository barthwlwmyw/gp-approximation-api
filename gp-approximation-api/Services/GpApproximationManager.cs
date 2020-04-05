using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp_approximation_api.Services
{
    public interface IApproximationTaskManager
    {
        Guid CreateTask(TaskParams taskParams);
        void RunTask(Guid taskGuid);
        IList<ApproximationTask> GetTasks();
        ApproximationTask GetTask(Guid taskGuid);
    }

    public class ApproximationTaskManager : IApproximationTaskManager
    {
        private readonly ILogger<ApproximationTaskManager> _logger;
        private IList<ApproximationTask> _approximationTasks { get; set; }

        public ApproximationTaskManager(ILogger<ApproximationTaskManager> logger)
        {
            _logger = logger;
            _approximationTasks = new List<ApproximationTask>();
        }

        public Guid CreateTask(TaskParams taskParams)
        {
            var taskGuid = Guid.NewGuid();

            var newTask = new ApproximationTask { TaskGuid = taskGuid, DataFilePath = taskParams.DataFilePath };

            _approximationTasks.Add(newTask);

            _logger.LogDebug($"Approximation task created, GUID: {taskGuid}");

            return newTask.TaskGuid;
        }

        public IList<ApproximationTask> GetTasks() => _approximationTasks.ToList();

        public ApproximationTask GetTask(Guid taskGuid) => _approximationTasks.Where(at => at.TaskGuid == taskGuid).Single();

        public void RunTask(Guid taskGuid)
        {
            //TODO: implementation
            _logger.LogDebug($"Approximation task fired, GUID: {taskGuid}");
        }

    }

    public class TaskParams
    {
        public string DataFilePath { get; set; }
    }

    public class ApproximationTask
    {
        public Guid TaskGuid { get; set; }
        public int TaskProgress { get; set; }
        public string DataFilePath { get; set; }
        public string ResultFilePath { get; set; }
        public bool IsDone { get; set; }
        public int Progress { get; set; }
    }

    public class GpApproximationManager
    {
        public GpApproximationManager()
        {
            ApproximationTasks = new List<ApproximationTask>();
        }

        public static List<ApproximationTask> ApproximationTasks { get; set; }

        public static void UpdateTaskProgress(Guid taskGuid, int progress)
        {
            ApproximationTasks.Where(at => at.TaskGuid == taskGuid).FirstOrDefault().TaskProgress = progress;
        }
    }

    
}
