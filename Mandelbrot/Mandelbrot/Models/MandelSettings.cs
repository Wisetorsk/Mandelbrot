using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mandelbrot.Models
{
    public class MandelSettings
    {
        [DisplayName("Range")]
        public int Range { get; set; }
        [DisplayName("Number of iterations")]
        public int NumIterations { get; set; }
        [DisplayName("Limit")]
        public int Limit { get; set; }
        [DisplayName("x center")]
        public double? xCenter { get; set; } = null;
        [DisplayName("y center")]
        public double? yCenter { get; set; } = null;
        [DisplayName("Radius")]
        public double? R { get; set; } = null;
    }
}
