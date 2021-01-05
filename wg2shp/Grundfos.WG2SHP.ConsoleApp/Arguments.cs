using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NLog;

namespace Grundfos.WG2SVG.ConsoleApp
{
    public class Arguments
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private readonly List<string> args;

        public Arguments(params string[] args)
        {
            this.args = args?.ToList() ?? new List<string>();
        }

        public bool ContainsKey(string key)
        {
            return this.args.Contains(key, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGetValue<T>(string key, out T value) where T : IConvertible
        {
            value = default(T);
            int index = this.args.FindIndex(x => x.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (index < 0)
            {
                return false;
            }

            if (this.args.Count < index + 2)
            {
                return false;
            }

            var rawValue = this.args[index + 1];
            value = (T)Convert.ChangeType(rawValue, typeof(T), CultureInfo.InvariantCulture);
            return true;
        }
    }
}
