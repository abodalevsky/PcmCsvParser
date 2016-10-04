using System;
using System.Collections.Generic;

namespace pcmcsvparse
{
    public class ArgsParser
    {
        string _fileName;

        public List<KeyValuePair<string,float>> Parameters
        {
            get;
            private set;
        }

        public ArgsParser(string[] args)
        {
            Parameters = new List<KeyValuePair<string, float>>();
            for (int i = 0; i < args.Length;)
            {
                string token = args[i];

                if (!parseParam(token, args, ref i))
                    break;
            }
        }

        bool parseParam(string token, string[] args, ref int idx)
        {
            if (token.ToLower() == "-f")
            {
                if (idx + 1 < args.Length)
                {
                    _fileName = args[idx + 1];
                    idx += 2;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (token.ToLower() == "-p")
            {
                if (idx + 2 < args.Length)
                {
                    string par = args[idx + 1];
                    string val = args[idx + 2];
                    idx += 3;

                    float f = 0;
                    Single.TryParse(val, out f);
                    Parameters.Add(new KeyValuePair<string, float>(par, f));

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string GetFileName()
        {
            return _fileName;
        }
    }
}
