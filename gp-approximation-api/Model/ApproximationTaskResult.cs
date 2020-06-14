using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace gp_approximation_api.Model
{
    public class ApproximationTaskResult
    {
        public string BestResult { get; set; }

        public double[] EvaluatedValues { get; set; }

        public int ValuesNumber { get; set; }

        public List<GenerationMetadata> AlgorithmRunMetadata { get; set; }

        public ApproximationTaskResult()
        {
            AlgorithmRunMetadata = new List<GenerationMetadata>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GenerationMetadata
    {
        public double BestFitness { get; set; }
        public double BestFitnessInGeneration { get; set; }
    }
}
