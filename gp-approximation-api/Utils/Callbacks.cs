using gp_approximation_api.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace gp_approximation_api.Utils
{
    public delegate void OnApproximationStartedCallback();

    public delegate void OnApproximationProgressUpdateCallback(
        StringBuilder taskGuid,
        int approximationProgress,
        IntPtr evaluatedValues,
        int evaluatedValuesLength,
        GenerationMetadata generationMetadata);

    public delegate void OnApproximationFinishedCallback(
        StringBuilder taskGuid);

    public delegate void OnApproximationFailedCallback();
}