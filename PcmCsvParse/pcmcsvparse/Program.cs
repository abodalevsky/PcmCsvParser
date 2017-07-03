using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pcmcsvparse
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
            if (args.Length < 5)
            {
                printBanner();
                Environment.Exit((int)ExitCode.WrongArguments);
            }

            try
            {
                var argsParser = new ArgsParser(args);
                ICsvParser csvParser;
                if (argsParser.HeaderLine == 1)
                    csvParser = new PcmCsvParser(argsParser.GetFileName());
                else
                    csvParser = new GpuCsvParser(argsParser.GetFileName());

                var metrix = argsParser.Parameters;
                foreach(var metric in metrix)
                {
                    var realValue = csvParser.GetMax(metric.Key);
                    if (realValue > metric.Value)
                    {
                        Console.WriteLine($"METRIC: {metric.Key} exceeded threshold {metric.Value} (real: {realValue})");
                        Environment.Exit((int)ExitCode.ExceededThreshold);
                    }
                }

                Console.WriteLine("Ok");
                Environment.Exit((int)ExitCode.Success);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during parsing {e.Message}");
                Environment.Exit((int)ExitCode.ExceededThreshold);
            }

        }

        static void printBanner()
        {
            Console.WriteLine("pcmcsvparse -f filename -p PAR_NAME PAR_VALUE [-p PAR_NAME PAR_VALUE]");
        }
    }
}
