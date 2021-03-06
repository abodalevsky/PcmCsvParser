﻿using System.IO;

namespace pcmcsvparse
{
    public class PcmCsvParser : CsvParser
    {

        public PcmCsvParser(string file)
            : base (file)
        {

            if (_table.Length < 3)
                throw new EndOfStreamException("Not enought data");

            int lastProcessedColumn = 0;
            var cap0 = _table.GetValue(0) as string[];
            var cap1 = _table.GetValue(1) as string[];

            while (lastProcessedColumn < cap0.Length)
            {
                _captions[cap1[lastProcessedColumn].ToUpper()] = lastProcessedColumn;

                ++lastProcessedColumn;
                if (!string.IsNullOrEmpty(cap0[lastProcessedColumn]))
                    break;
            }
        }
    }
}
