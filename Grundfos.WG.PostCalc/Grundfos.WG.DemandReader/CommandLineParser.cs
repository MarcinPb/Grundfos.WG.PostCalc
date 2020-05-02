using System;
using System.Collections.Generic;
using System.Linq;

namespace Grundfos.WG.DemandReader
{
    public class CommandLineParser
    {
        private readonly List<string> args;

        public CommandLineParser(string[] args)
        {
            this.args = args.ToList();
        }

        public bool ContainsSwitch(string key)
        {
            return this.args.Contains(key, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default(T);
            int keyIndex = this.args.FindIndex(x => x.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (keyIndex < 0 || this.args.Count < keyIndex - 2)
            {
                return false;
            }

            string rawValue = this.args[keyIndex + 1];
            value = (T)Convert.ChangeType(rawValue, typeof(T));
            return true;
        }
    }
}
