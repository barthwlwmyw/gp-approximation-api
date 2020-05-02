using gp_approximation_api.Model;
using gp_approximation_api.Repos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace gp_approximation_api.Services
{
    public interface IApproximationTaskManager
    {
        Guid CreateTask(Guid taskGuid, string dataFilePath, AlgorithmParams algorithmParams);
        void RunTask(Guid taskGuid);
        IList<ApproximationTask> GetTasks();
        ApproximationTask GetTask(Guid taskGuid);
    }

    public class ApproximationTaskManager : IApproximationTaskManager
    {
        private readonly ILogger<ApproximationTaskManager> _logger;
        private readonly IApproximationProvider _approximationProvider;
        private readonly IApproximationTaskRepository _taskRepository;

        private IList<ApproximationTask> _approximationTasks { get; set; }


        public ApproximationTaskManager(ILogger<ApproximationTaskManager> logger, IApproximationProvider approximationProvider, IApproximationTaskRepository taskRepository)
        {
            _logger = logger;
            _approximationTasks = new List<ApproximationTask>();
            _approximationProvider = approximationProvider;
            _taskRepository = taskRepository;
        }

        public Guid CreateTask(Guid taskGuid, string dataFilePath, AlgorithmParams algorithmParams)
        {
            var newTask = new ApproximationTask { 
                TaskGuid = taskGuid, 
                DataFilePath = dataFilePath,
                AlgorithmParams = algorithmParams,
                TaskProgress = 1
            };

            _taskRepository.AddTask(newTask);

            return newTask.TaskGuid;
        }

        public IList<ApproximationTask> GetTasks() => _taskRepository.GetAllTasks();

        public ApproximationTask GetTask(Guid taskGuid) => _taskRepository.GetApproximationTask(taskGuid);

        public void RunTask(Guid taskGuid)
        {
            var task = _taskRepository.GetApproximationTask(taskGuid);

            //TODO: implementation
            _logger.LogDebug($"Approximation task fired, GUID: {taskGuid}");

            //test implementation:
            Task.Run(() => _approximationProvider.Approximate(
                UpdateTaskStatus,
                FinalizeTask,
                taskGuid,
                task.DataFilePath,
                task.AlgorithmParams));
        
        }

        private void UpdateTaskStatus(StringBuilder taskGuid, int progress, IntPtr evaluatedValues, int evaluatedValuesLength)
        {
            var outputs = new double[evaluatedValuesLength];

            Marshal.Copy(evaluatedValues, outputs, 0, evaluatedValuesLength);

            Console.WriteLine($"UpdateTaskStatusCalled with progress: {progress}, guid: {taskGuid}, evaluated values: {string.Join(' ', outputs)}");
            _taskRepository.UpdateTaskProgress(Guid.Parse(taskGuid.ToString()), progress);
        }

        private void FinalizeTask(StringBuilder taskGuid)
        {
            Console.WriteLine($"Finalize task callback called: {taskGuid}");
            _taskRepository.FinalizeTask(Guid.Parse(taskGuid.ToString()));
        }
    }
}
