using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ccmetricsparse
{
    class Program
    {
        enum ExitCode : int
        {
            Success = 0,
            WrongArguments = 1,
            ExceededThreshold = 2
        };

        static void Main(string[] args)
        {
            var argsParser = new ArgsParser(args);

            if (argsParser.BailOutMax == 0 && argsParser.RejitMax == 0)
            {
                printBanner();
                Environment.Exit((int)ExitCode.WrongArguments);
            }

            var lineParser = new CCOutParser();

            while (true)
            {
                if (!lineParser.ParseLine(Console.ReadLine()))
                    break;
            }

            Console.WriteLine("Metric \t| Max \t | Real");
            Console.WriteLine($"BailOut\t| {argsParser.BailOutMax} \t | {lineParser.BailOut}");
            Console.WriteLine($"ReJit \t| {argsParser.RejitMax} \t | {lineParser.ReJit}");

            if (lineParser.BailOut < argsParser.BailOutMax &&
                lineParser.ReJit < argsParser.RejitMax)
            {
                Console.WriteLine("Ok");
                Environment.Exit((int)ExitCode.Success);
            }
            else
            {
                Console.WriteLine("Threshold exceedded!");
                Environment.Exit((int)ExitCode.ExceededThreshold);
            }
        }

        static void printBanner()
        {
            Console.WriteLine("ccmetricsparse [-BailOut: maxValue] [-ReJit: maxValue]");
        }

    }
}
