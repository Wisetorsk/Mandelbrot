using System;
using System.Globalization;
using MandelbrotGenerator;

namespace ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments provided. ");
                    return (int)ExitCode.NoArguments;
                }

                if (!int.TryParse(args[1], out int resolution))
                {
                    Console.WriteLine("Unable to parse resolution");
                    return (int)ExitCode.ParseError;
                }
                if (resolution <= 0)
                {
                    Console.WriteLine("Resolution argument must be positive and non zero");
                    return (int)ExitCode.InvalidResolutionArgument;
                }

                if (!int.TryParse(args[2], out int numIterations))
                {
                    Console.WriteLine("Unable to parse number of iterations");
                    return (int)ExitCode.ParseError;
                }
                if (numIterations <= 0)
                {
                    Console.WriteLine("Number of iterations must be positive and non zero");
                    return (int)ExitCode.InvalidIterationsArgument;
                }
                
                if (!int.TryParse(args[3], out int limit))
                {
                    Console.WriteLine("Unable to parse limit argument");
                    return (int)ExitCode.ParseError;
                }
                if (limit <= 0)
                {
                    Console.WriteLine("Limit argument must be positive and non zero");
                    return (int)ExitCode.InvalidLimitArgument;
                }

                if (!double.TryParse(args[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double xCenter))
                {
                    Console.WriteLine("Unable to parse X center argument");
                    return (int)ExitCode.ParseError;
                }

                if (!Double.TryParse(args[5], NumberStyles.Any, CultureInfo.InvariantCulture, out double yCenter))
                {
                    Console.WriteLine("Unable to parse Y center argument");
                    return (int)ExitCode.ParseError;
                }

                if (!Double.TryParse(args[6], NumberStyles.Any, CultureInfo.InvariantCulture, out double radius))
                {
                    Console.WriteLine("Unable to parse radius argument");
                    return (int)ExitCode.ParseError;
                }
                Mandelbrot.ParallelMandelbrot(resolution, numIterations, limit, xCenter, yCenter, radius);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please follow this pattern: <mandel/julia> <resolution:int> <iterations:int> <limit:int> <x:double> <y:double> <radius:double>");
                Console.WriteLine("(for julia)  <mandel/julia> <resolution:int> <iterations:int> <limit:int> <x:double> <y:double> <radius:double> <real:double> <imag:double>");
                return (int)ExitCode.GeneralError;
            }
            
            return (int)ExitCode.Success;
        }
    }
}
