using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp_approximation_api.Utils
{
    public delegate void onApproximationStartedCallback();

    public delegate void onApproximationProgressUpdateCallback(int approximationProgress);

    public delegate void onApproximationFinishedCallback();

    public delegate void onApproximationFailedCallback();
}