using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotGenerator
{
    public class Geometry
    {
        /// <summary>
        /// Generates a set of points to draw a circle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="R"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static Point[] Circle(int x, int y, double R, int steps=1000)
        {
            double end = 2 * Math.PI;
            Point[] points = new Point[steps];
            for (int i = 0; i < steps; i++)
            {
                double theta = i*end / steps;
                int xPoint = x + (int)Math.Floor(R * Math.Cos(theta));
                int yPoint = y + (int)Math.Floor(R * Math.Sin(theta));
                points[i] = new Point(xPoint, yPoint);
            }
            return points;
        }

        /// <summary>
        /// Generates a set of points to draw a square.
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yStart"></param>
        /// <param name="yEnd"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static Point[] Square(int xStart, int xEnd, int yStart, int yEnd, int steps=100)
        {
            Point[] points = new Point[steps];

            return points;
        }

        public static Point[] Square(int xStart, int xEnd, int length, int steps = 100)
        {
            Point[] points = new Point[steps];

            return points;
        }

        /// <summary>
        /// Generate square based on a central point, width and height. 
        /// </summary>
        /// <param name="centre"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static Point[] Square(Point centre, int width, int height, int steps = 100)
        {
            Point[] points = new Point[steps];

            return points;
        }
    }
}
