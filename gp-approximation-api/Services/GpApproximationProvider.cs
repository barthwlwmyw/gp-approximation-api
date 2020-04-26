using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using gp_approximation_api.Utils;

namespace gp_approximation_api.Services
{
    public interface IApproximationProvider
    {
        void Approximate(
            OnApproximationProgressUpdateCallback onProgressUpdate, 
            OnApproximationFinishedCallback onApproximationFinished,
            Guid taskGuid,
            string datafilePath);
    }
    public class ApproximationProvider : IApproximationProvider
    {
        public void Approximate(
            OnApproximationProgressUpdateCallback onProgressUpdate, 
            OnApproximationFinishedCallback onApproximationFinished,
            Guid taskGuid,
            string datafilePath)
        {
            StartApproximation(
                onProgressUpdate,
                onApproximationFinished,
                taskGuid.ToString(),
                datafilePath);
        }

        [DllImport(@"gp-approximation-engine.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "callback_test")]
        static extern void StartApproximation(
            [MarshalAs(UnmanagedType.FunctionPtr)]OnApproximationProgressUpdateCallback onProgressUpdate,
            [MarshalAs(UnmanagedType.FunctionPtr)]OnApproximationFinishedCallback onApproximationFinished,
            [MarshalAs(UnmanagedType.LPStr)] string taskGuid,
            [MarshalAs(UnmanagedType.LPStr)] string datafilePath);
    }
}
