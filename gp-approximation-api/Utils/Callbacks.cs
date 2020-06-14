using gp_approximation_api.Model;
using System;
using System.Text;

namespace gp_approximation_api.Utils
{
    public delegate void OnApproximationProgressUpdateCallback(
        StringBuilder taskGuid,
        int approximationProgress,
        IntPtr evaluatedValues,
        int evaluatedValuesLength,
        GenerationMetadata generationMetadata);

    public delegate void OnApproximationFinishedCallback(
        StringBuilder taskGuid,
        StringBuilder treeFormula);
}