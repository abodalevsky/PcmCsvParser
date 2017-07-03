using System.IO;

namespace pcmcsvparse
{
    public class GpuCsvParser : CsvParser
    {
        public GpuCsvParser(string file)
            : base (file)
        {
            if (_table.Length < 2)
                throw new EndOfStreamException("Not enought data");

            int lastProcessedColumn = 0;
            var cap0 = _table.GetValue(0) as string[];
            while (lastProcessedColumn < cap0.Length)
            {
                _captions[cap0[lastProcessedColumn].ToUpper().Trim()] = lastProcessedColumn;
                ++lastProcessedColumn;
            }
        }

    }
}
