using System;
using System.Linq;
using System.Threading.Tasks;

namespace MandelbrotGenerator
{
    public class Mandelbrot
    {
        public static void ParallelMandelbrot(int range, int numIterations = 100, int limit = 4, double? xCenter = null, double? yCenter = null, double? R = null, string filename = null)
        {
            double xstart;// 0.5; //-2
            double xend;// 0.2; //1
            double ystart;// 0.1; // -1.5
            double yend;
            if (xCenter is null || yCenter is null || R is null)
            {
                xstart = -2;// 0.5; //-2
                xend = 1;// 0.2; //1
                ystart = -1.5;// 0.1; // -1.5
                yend = 1.5;// .4; // 1.5
                filename = (filename is null) ? "Mandelbrot" : filename;
            }
            else
            {
                xstart = (double)xCenter - (double)R;
                ystart = (double)yCenter - (double)R;
                xend = (double)xCenter + (double)R;
                yend = (double)yCenter + (double)R;
                filename = (filename is null) ? $"Mandelbrot[X{xCenter.ToString().Replace(".", ",")}_Y{yCenter.ToString().Replace(".", ",")}]R{R.ToString().Replace(".", ",")}" : filename;
            }

            Console.WriteLine($"Calculating Mandelbrot set at x: {xCenter} y:{yCenter} Radius: {R} Iterations: {numIterations}\nOutput resolution {range}x{range} Output filename: {filename}.png\nPlease wait...");
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

                var val = mandel(i.Item3, i.Item4, numIterations, limit);
                bmp.InsertPixel(i.Item1, i.Item2, val);
            });
            Console.WriteLine("Calculation complete, saving image...");
            bmp.Save();
        }

        private static byte mandel(double r, double i, int maxIterations, int limit)
        {
            double x0, y0, x, y, xlast, ylast;

            var n = 0;
            x0 = r;
            y0 = i;
            x = x0;
            y = y0;
            xlast = 0;
            ylast = 0;
            while (n < maxIterations && x * x + y * y <= limit)
            {
                x = (xlast * xlast) - (ylast * ylast) + x0;
                y = 2 * xlast * ylast + y0;

                xlast = x;
                ylast = y;
                n = n + 1;
            }
            return (byte)Math.Ceiling((double)n / maxIterations * 255);
        }

    }
}
