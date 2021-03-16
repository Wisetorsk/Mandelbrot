using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Flags]
    public enum ExitCode : int
    {
        Success = 0,
        NoArguments = 1,
        GeneralError = 2,
        InvalidResolutionArgument = 3,
        InvalidIterationsArgument = 4,
        InvalidLimitArgument = 5,
        InvalidXcenterArgument = 6,
        InvalidYcenterArgument = 7,
        InvalidRArgument = 8,
        InvalidFilename = 9,
        ParseError = 10
    }
}
