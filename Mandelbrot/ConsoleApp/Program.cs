using System;
using System.Globalization;
using MandelbrotGenerator;

namespace ConsoleApp
{
    class Program
    {
        static string helptext = @"
|    This program generates png images of the mandelbrot set.                                                                       |
|    Use:                                                                                                                           |
|        mandelbrot <resolution:int> <iterations:int> <limit:int> <x:double> <y:double> <radius:double>                             |
|                                                                                                                                   |
|    The program will create a png file of the resulting set in the same folder as the executable.                                  |
|    The program can also create images of the Julia set by specifying two additional arguments:                                    |
|    Use:                                                                                                                           |
|        julia <resolution:int> <iterations:int> <limit:int> <x:double> <y:double> <radius:double> <real:double> <imag:double>      |";
        static int Main(string[] args)
        {
            try
            {
                Help(args);
                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments provided. ");
                    return (int)ExitCode.NoArguments;
                }

                var mode = args[0].ToLower().Trim();
                if (mode != "mandelbrot" && mode != "julia")
                {
                    Console.WriteLine("Unable to parse mode argument");
                    return (int)ExitCode.ParseError;
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

                if (mode == "mandelbrot")
                {
                    string filename;
                    try
                    {
                        filename = args[7];
                        if (filename.Length < 1 || filename is null) throw new Exception("Wrong filename");
                        Mandelbrot.ParallelMandelbrot(resolution, numIterations, limit, xCenter, yCenter, radius, filename);
                    }
                    catch (Exception)
                    {
                        Mandelbrot.ParallelMandelbrot(resolution, numIterations, limit, xCenter, yCenter, radius);
                    }
                } else
                {
                    if (!Double.TryParse(args[7], NumberStyles.Any, CultureInfo.InvariantCulture, out double real))
                    {
                        Console.WriteLine("Unable to parse real number argument");
                        return (int)ExitCode.ParseError;
                    }

                    if (!Double.TryParse(args[8], NumberStyles.Any, CultureInfo.InvariantCulture, out double imag))
                    {
                        Console.WriteLine("Unable to parse imaginary number argument");
                        return (int)ExitCode.ParseError;
                    }
                    string filename;
                    try
                    {
                        filename = args[9];
                        if (filename.Length < 1 || filename is null) throw new Exception("Wrong filename");
                        Julia.ParallelJulia(resolution, real, imag, numIterations, limit, xCenter, yCenter, radius);
                    }
                    catch (Exception)
                    {
                        Julia.ParallelJulia(resolution, real, imag, numIterations, limit, xCenter, yCenter, radius);
                    }

                }

            }
            catch (IndexOutOfRangeException)
            {
                Help(args);
                return (int)ExitCode.GeneralError;
            }
            
            return (int)ExitCode.Success;
        }

        public static void Help(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.Contains("/h") || arg.Contains("help") || arg.Contains("/help") || arg.Contains(@"\h") || arg.Contains(@"\help") ||
                    arg.Contains("/H") || arg.Contains("HELP") || arg.Contains("/HELP") || arg.Contains(@"\H") || arg.Contains(@"\HELP"))
                {
                    Console.WriteLine(new string('=', Console.WindowWidth));
                    Console.WriteLine($"{new string(' ', Console.WindowWidth/2 - 15)}Mandelbrot set image generator{new string(' ', Console.WindowWidth/2 - 15)}");
                    Console.WriteLine(new string('-', Console.WindowWidth));
                    Console.WriteLine(helptext);
                    Console.WriteLine(new string('=', Console.WindowWidth));
                }
            }
        }
    }
}
