using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ccmetricsparse
{
    public class CCOutParser
    {
        enum State
        {
            Next,
            Rejit,
            BailOut
        }

        public int ReJit { get; private set; }
        public int BailOut { get; private set; }


        State state = State.Next;

        public bool ParseLine (string line)
        {
            if (line == null)
                return false;

            if (state == State.Next)
            {
                ExpectHeader(line);
            }
            else if (state == State.Rejit)
            {
                ReJit = ParseTotal(line);
            }
            else if (state == State.BailOut)
            {
                BailOut = ParseTotal(line);
            }
            else
            {
                throw new System.InvalidOperationException("Uncpecified state. Terminating...");
            }

            return true;
        }

        void ExpectHeader(string line)
        {
            const string RejitHeader = "Rejit Reason,";
            const string BailoutHeader = "Bailout Reason,";

            if (line.StartsWith(BailoutHeader))
            {
                state = State.BailOut;
            }
            else if (line.StartsWith(RejitHeader))
            {
                state = State.Rejit;
            }
        }

        int ParseTotal(string line)
        {
            const string TotlaHeader = "TOTAL,";

            if (!line.StartsWith(TotlaHeader))
                return 0;

            state = State.Next;
            const string pattern = @"\d+";

            foreach (Match m in Regex.Matches(line, pattern))
            {
                int res = 0;
                if (Int32.TryParse(m.Value, out res))
                    return res;
            }

            return 0;
        }
    }
}
