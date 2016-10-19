using System;
using System.Collections.Generic;

namespace ccmetricsparse
{
    public class ArgsParser
    {
        public int RejitMax { get; private set; }
        public int BailOutMax { get; private set; }

        public ArgsParser(string[] args)
        {
            for (int i = 0; i < args.Length-1;)
            {
                string token = args[i];

                int val = 0;
                Int32.TryParse(args[i + 1], out val);
                i += 2;

                if (val == 0)
                    continue;


                if (token.ToUpper() == "-BAILOUT:")
                {
                    BailOutMax = val;
                }
                else if (token.ToUpper() == "-REJIT:")
                {
                    RejitMax = val;
                }
            }
        }
    }
}
