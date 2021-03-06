﻿using Ideafixxxer.CsvParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace pcmcsvparse
{
    public class CsvParser : ICsvParser
    {
        readonly protected CsvParser2 _parser;
        readonly protected Array _table;
        readonly protected Dictionary<string, int> _captions = new Dictionary<string, int>(22);

        /// <summary>
        /// Parses CSV file
        /// </summary>
        /// <param name="file">path to file</param>
        /// <exception cref="System.IO.IOException"> will be thrown if file wasn't found</exception>
        public CsvParser(string file)
        {
            _parser = new CsvParser2();

            using (var reader = new StreamReader(file))
            {
                _table = _parser.Parse(reader);
            }

        }

        public bool HasColumn(string name)
        {
            return _captions.ContainsKey(name.ToUpper());
        }

        /// <summary>
        /// Return maximum value for given column
        /// </summary>
        /// <param name="column">column name</param>
        /// <returns>maximum value</returns>
        /// <exception cref="KeyNotFoundException">In case if column doesn't exists</exception>
        public float GetMax(string column)
        {
            float val = 0;
            int col = _captions[column.ToUpper()];

            for (int i = 2; i < _table.Length; i++) //i = 2 means we miss rows with caption
            {
                var row = _table.GetValue(i) as string[];
                float n = 0;
                Single.TryParse(row[col], out n);
                val = Math.Max(val, n);
            }

            return val;
        }

    }
}
