using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace gp_approximation_api.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public class TestClass
    {
        public double BestValue { get; set; }
        public double AverageValue { get; set; }
        public double WorstValue { get; set; }
    }
}
