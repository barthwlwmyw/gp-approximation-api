using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp_approximation_api.Model
{
    public class ApproximationTaskResult
    {
        public string BestResult { get; set; }

        public double[] EvaluatedValues { get; set; }

        public int ValuesNumber { get; set; }

        public List<AlgorithmProgress> AlgorithmRunMetadata { get; set; }

        public ApproximationTaskResult()
        {
            AlgorithmRunMetadata = new List<AlgorithmProgress>();
        }
    }

    public class AlgorithmProgress
    {
        double BestFitnessValue { get; set; }
        double AverageFitnessValue { get; set; }
        double WorstFitnessValue { get; set; }
    }
}
