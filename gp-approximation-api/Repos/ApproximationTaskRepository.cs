using gp_approximation_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp_approximation_api.Repos
{
    public interface IApproximationTaskRepository
    {
        void AddTask(ApproximationTask task);
        void UpdateTask(ApproximationTask task);
        ApproximationTask GetApproximationTask(Guid taskGuid);
        List<ApproximationTask> GetAllTasks();
        void UpdateTaskProgress(Guid taskGuid, int progress);
        void FinalizeTask(Guid taskGuid);
    }

    public class ApproximationTaskRepository: IApproximationTaskRepository
    {
        private IList<ApproximationTask> _approximationTasks;

        public ApproximationTaskRepository()
        {
            _approximationTasks = new List<ApproximationTask>();
        }

        public void AddTask(ApproximationTask task)
        {
            _approximationTasks.Add(task);
        }

        public List<ApproximationTask> GetAllTasks()
        {
            return _approximationTasks.ToList();
        }

        public ApproximationTask GetApproximationTask(Guid taskGuid)
        {
            return _approximationTasks.Single(at => at.TaskGuid == taskGuid);
        }

        public void UpdateTask(ApproximationTask task)
        {
            var taskToUpdate = _approximationTasks.First(at => at.TaskGuid == task.TaskGuid);
            taskToUpdate = task;
        }

        public void UpdateTaskProgress(Guid taskGuid, int progress)
        {
            _approximationTasks.Single(at => at.TaskGuid == taskGuid).TaskProgress = progress;
        }

        public void FinalizeTask(Guid taskGuid)
        {
            _approximationTasks.Single(at => at.TaskGuid == taskGuid).IsDone = true;
        }
    }
}
