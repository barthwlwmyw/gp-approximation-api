using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using gp_approximation_api.Utils;

namespace gp_approximation_api.Services
{
    public interface IApproximationProvider
    {
        void Approximate(onApproximationProgressUpdateCallback onProgressUpdate);
    }
    public class ApproximationProvider : IApproximationProvider
    {

        [DllImport(@"gp-approximation-engine.dll", EntryPoint = "callback_test")]
        static extern void StartApproximation([MarshalAs(UnmanagedType.FunctionPtr)]onApproximationProgressUpdateCallback onProgressUpdate);


        public void Approximate(onApproximationProgressUpdateCallback onProgressUpdate)
        {
            Task.Run(() => StartApproximation(onProgressUpdate));

        }
    }
}
