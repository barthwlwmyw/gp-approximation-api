using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gp_approximation_api.Model
{
    public class AlgorithmParams
    {
        [JsonPropertyName("populationSize")]
        public int PopulationSize { get; set; }
        [JsonPropertyName("generationsNumber")]
        public int GenerationsNumber { get; set; }
        [JsonPropertyName("crossoverProbability")]
        public double CrossoverProbability { get; set; }
        [JsonPropertyName("mutationProbability")]
        public double MutationProbability { get; set; }
    }
}
