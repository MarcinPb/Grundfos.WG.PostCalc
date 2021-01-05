using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NLog;

namespace Grundfos.TW.DataSourceMap.Tw2Opc
{
    public class TwVar2IDMapReader
    {
        public const string IcorrectEntryMessage = "Incomplete entry in line {0}: '{1}'";
        public const string DuplicateEntryMessage = "Skipped duplicate entry in line {0}: '{1}'";

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private readonly string fileName;

        public TwVar2IDMapReader(string fileName, int codePage = 1250, string separator = "<->")
        {
            this.Separator = new string[] { separator };
            this.fileName = fileName;
            this.CodePage = codePage;
        }

        public string[] Separator { get; }

        public int CodePage { get; }

        public List<TwVar2IDMapEntry> Read()
        {
            log.Info("Start reading TwVar2ID map entries from: {0}", this.fileName);
            var lines = File.ReadAllLines(this.fileName, Encoding.GetEncoding(this.CodePage));
            var result = new Dictionary<string, TwVar2IDMapEntry>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var split = line.Split(this.Separator, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2)
                {
                    log.Trace(IcorrectEntryMessage, i + 1, line);
                    continue;
                }

                string name = split[0].Trim();
                if (name.Length < 1)
                {
                    log.Trace(IcorrectEntryMessage, i + 1, line);
                    continue;
                }

                if (!long.TryParse(split[1].Trim(), out long id))
                {
                    log.Trace(IcorrectEntryMessage, i + 1, line);
                    continue;
                }

                if (result.TryGetValue(name, out TwVar2IDMapEntry mapEntry))
                {
                    log.Trace(DuplicateEntryMessage, i + 1, line);
                    continue;
                }

                result[name] = new TwVar2IDMapEntry { VariableName = name, VariableID = id };
            }

            log.Info("Finished reading TwVar2ID map entries. {0} entries were found.", result.Count);
            return result.Values.ToList();
        }
    }
}
