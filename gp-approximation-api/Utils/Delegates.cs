using System.Text;

namespace gp_approximation_api.Utils
{
    public delegate void OnApproximationStartedCallback();

    public delegate void OnApproximationProgressUpdateCallback(StringBuilder taskGuid, int approximationProgress);

    public delegate void OnApproximationFinishedCallback(StringBuilder taskGuid);

    public delegate void OnApproximationFailedCallback();
}