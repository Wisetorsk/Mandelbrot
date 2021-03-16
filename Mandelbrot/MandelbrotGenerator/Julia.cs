using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotGenerator
{
    class Julia
    {
        public static void ParallelJulia(int range, double r, double imag, int numIterations = 100, int limit = 4, double? xCenter = null, double? yCenter = null, double? R = null, string filename = "pJulia")
        {
            {

                double xstart;
                double xend;
                double ystart;
                double yend;
                if (xCenter is null || yCenter is null || R is null)
                {
                    xstart = -1.5;
                    xend = 1.5;
                    ystart = -1.5;
                    yend = 1.5;
                }
                else
                {
                    xstart = (double)xCenter - (double)R;
                    ystart = (double)yCenter - (double)R;
                    xend = (double)xCenter + (double)R;
                    yend = (double)yCenter + (double)R;
                    filename += $"[X{xCenter.ToString().Replace(".", ",")}_Y{yCenter.ToString().Replace(".", ",")}]R{R.ToString().Replace(".", ",")}";
                }

                Console.WriteLine($"Calculating Julia set at x: {xCenter} y:{yCenter} Radius: {R} Iterations: {numIterations}\nOutput resolution {range}x{range} Output filename: {filename}.png\nPlease wait...");
                var irange = Enumerable.Range(0, range).ToArray();
                var indexes = irange.Select(i => (i, Enumerable.Range(0, range)));
                var i = irange.Select(i => xstart + (xend - xstart) * ((double)i / (irange.Length - 1))).ToArray();
                var j = irange.Select(i => ystart + (yend - ystart) * ((double)i / (irange.Length - 1))).ToArray();
                var xLength = i.Count();
                var yLength = j.Count();
                var bmp = new Bitmapper(xLength, filename, false);//new BitmapGenerator(xLength, yLength, "mandelbrot");
                var values = new (int, int, double, double)[xLength * yLength];
                byte[] imgData = new byte[xLength * yLength];
                var ind = 0;
                for (int x = 0; x < xLength; x++)
                {
                    for (int y = 0; y < yLength; y++)
                    {
                        values[ind] = (x, y, i[x], j[y]);
                        ind++;
                    }
                }


                Parallel.ForEach(values, i => {

                    var val = juli(i.Item3, i.Item4, r, imag, numIterations, limit);
                    bmp.InsertPixel(i.Item1, i.Item2, val);
                });
                Console.WriteLine("Calculation complete, saving image...");
                bmp.Save();
            }
        }


        private static byte juli(double r, double i, double real, double imag, int maxIterations, int limit)
        {
            var n = 0;
            var x = r;
            var y = i;
            double xTemp = 0;
            while (n < maxIterations && x * x + y * y <= limit)
            {
                xTemp = x * x - y * y;
                y = 2 * x * y + imag;
                x = xTemp + real;
                n++;
            }

            //byte value = (n > (maxIterations*0.95)) ? (byte)Math.Ceiling((double)n / maxIterations * 255) : (byte)0;
            return (byte)Math.Ceiling((double)n / maxIterations * 255);
        }
    }
}
