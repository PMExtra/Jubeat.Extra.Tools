using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jubeat.Extra.Models.Maps.Memo;
using static Jubeat.Extra.Models.Maps.Memo.MemoDefinitions;

namespace Jubeat.Extra.IO
{
    public class MemoMapReader : IDisposable
    {
        private readonly StreamReader _sr;
        private bool _back;

        private string _line;

        public MemoMapReader(Stream stream)
        {
            _sr = new StreamReader(stream, Encoding.UTF8);
        }

        public MemoMapReader(string path) : this(new FileStream(path, FileMode.Open))
        {
        }

        public bool EndOfStream => _sr.EndOfStream && !_back;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        private async Task<string> ReadLineAsync()
        {
            if (_back)
            {
                _back = false;
            }
            else
            {
                _line = (await _sr.ReadLineAsync()).Trim();
            }
            return _line;
        }

        private void BackLine()
        {
            _back = true;
        }

        public void Rewind()
        {
            _sr.BaseStream.Position = 0;
            _sr.DiscardBufferedData();
        }

        public async Task<MemoMap> ReadAllAsync()
        {
            Rewind();

            var map = new MemoMap();
            var lines = new List<Match>();
            while (!EndOfStream)
            {
                var line = await ReadLineAsync();

                var match = OrdinalRegex.Match(line);
                if (match.Success)
                {
                    if (lines.Count > 0)
                    {
                        var measure = MemoMeasure.Parse(lines);
                        measure.Ordinal = map.Measures.Count + 1;
                        map.Measures.Add(measure);
                        lines.Clear();
                    }
                    var ordinal = int.Parse(match.Groups["ordinal"].Value);
                    Debug.Assert(map.Measures.Count + 1 == ordinal);
                    continue;
                }

                match = MemoLineRegex.Match(line);
                if (match.Success)
                {
                    lines.Add(match);
                    continue;
                }

                Debug.Assert(string.IsNullOrEmpty(line));
            }

            return map;
        }
    }
}
