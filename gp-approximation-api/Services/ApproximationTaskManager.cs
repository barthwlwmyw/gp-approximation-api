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
        Guid CreateTask(AlgorithmParams algorithmParams);
        void AssignSourceFile(Guid taskGuid, string dataFilePath);
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

        public Guid CreateTask(AlgorithmParams algorithmParams)
        {
            var newTask = new ApproximationTask { 
                TaskGuid = Guid.NewGuid(),
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

            _logger.LogDebug($"Approximation task fired, GUID: {taskGuid}");

            Task.Run(() => _approximationProvider.Approximate(
                UpdateTaskStatus,
                FinalizeTask,
                taskGuid,
                task.DataFilePath,
                task.AlgorithmParams));
        
        }

        public void AssignSourceFile(Guid taskGuid, string dataFilePath)
        {
            var task = _taskRepository.GetApproximationTask(taskGuid);

            task.DataFilePath = dataFilePath;

            _taskRepository.UpdateTask(task);
        }

        private void UpdateTaskStatus(StringBuilder taskGuid, int progress, IntPtr evaluatedValuesPtr, int evaluatedValuesLength, GenerationMetadata generationMetadata)
        {
            var evaluatedValues = new double[evaluatedValuesLength];

            Marshal.Copy(evaluatedValuesPtr, evaluatedValues, 0, evaluatedValuesLength);

            Console.WriteLine($"UpdateTaskStatusCalled with progress: {progress}, guid: {taskGuid},struct: {generationMetadata.BestFitness}/{generationMetadata.BestFitnessInGeneration}");

            var task = _taskRepository.GetApproximationTask(Guid.Parse(taskGuid.ToString()));

            task.TaskProgress = progress;
            task.Result.EvaluatedValues = evaluatedValues;
            task.Result.ValuesNumber = evaluatedValuesLength;
            task.Result.AlgorithmRunMetadata.Add(generationMetadata);

            _taskRepository.UpdateTaskProgress(Guid.Parse(taskGuid.ToString()), progress);
        }

        private void FinalizeTask(StringBuilder taskGuid, StringBuilder treeFormula)
        {
            var taskToFinalize = _taskRepository.GetApproximationTask(Guid.Parse(taskGuid.ToString()));

            taskToFinalize.IsDone = true;
            taskToFinalize.TaskProgress = 100;
            taskToFinalize.Result.BestResult = treeFormula.ToString();

            _taskRepository.UpdateTask(taskToFinalize);

        }
    }
}
