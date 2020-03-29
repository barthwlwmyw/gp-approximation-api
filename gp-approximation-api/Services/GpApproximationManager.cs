using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp_approximation_api.Services
{
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

    public class ApproximationTask
    {
        public Guid TaskGuid { get; set; }
        public int TaskProgress { get; set; }
    }
}
