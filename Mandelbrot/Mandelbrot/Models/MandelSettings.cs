using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mandelbrot.Models
{
    public class MandelSettings
    {
        [DisplayName("Resolution")]
        public int Range { get; set; }
        [DisplayName("Number of iterations")]
        public int NumIterations { get; set; }
        [DisplayName("Limit")]
        public int Limit { get; set; }
        [DisplayName("x center")]
        public double xCenter { get; set; }
        [DisplayName("y center")]
        public double yCenter { get; set; }
        [DisplayName("Radius")]
        public double? R { get; set; } = null;
        [DisplayName("Real")]
        public double Real { get; set; }
        [DisplayName("Imaginary")]
        public double Imag { get; set; }
    }
}
