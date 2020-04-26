using System;

namespace gp_approximation_api.Model
{
    public class ApproximationTask
    {
        public Guid TaskGuid { get; set; }
        public int TaskProgress { get; set; }
        public string DataFilePath { get; set; }
        public bool IsDone { get; set; }
        public AlgorithmParams AlgorithmParams { get; set; }
    }
}
