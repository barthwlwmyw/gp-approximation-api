using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using gp_approximation_api.Model;
using gp_approximation_api.Utils;

namespace gp_approximation_api.Services
{
    public interface IApproximationProvider
    {
        void Approximate(
            OnApproximationProgressUpdateCallback onProgressUpdate, 
            OnApproximationFinishedCallback onApproximationFinished,
            Guid taskGuid,
            string datafilePath,
            AlgorithmParams algorithmParams);
    }
    public class ApproximationProvider : IApproximationProvider
    {
        public void Approximate(
            OnApproximationProgressUpdateCallback onProgressUpdate, 
            OnApproximationFinishedCallback onApproximationFinished,
            Guid taskGuid,
            string datafilePath,
            AlgorithmParams algorithmParams)
        {
            StartApproximation(
                onProgressUpdate,
                onApproximationFinished,
                taskGuid.ToString(),
                datafilePath,
                algorithmParams.PopulationSize,
                algorithmParams.GenerationsNumber,
                algorithmParams.CrossoverProbability,
                algorithmParams.MutationProbability);
        }

        [DllImport(@"gp-approximation-engine.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "callback_test")]
        static extern void StartApproximation(
            [MarshalAs(UnmanagedType.FunctionPtr)]OnApproximationProgressUpdateCallback onProgressUpdate,
            [MarshalAs(UnmanagedType.FunctionPtr)]OnApproximationFinishedCallback onApproximationFinished,
            [MarshalAs(UnmanagedType.LPStr)] string taskGuid,
            [MarshalAs(UnmanagedType.LPStr)] string datafilePath,
            int populationSize,
            int generationsNumber,
            double crossoverProbability,
            double mutationProbability);
    }
}
