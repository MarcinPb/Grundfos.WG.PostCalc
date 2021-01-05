using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using NPOI.SS.UserModel;

namespace Grundfos.TW.DataSourceMap.Tw2Opc
{
    public class TwOpcTagExtractor
    {
        private readonly Regex regex;
        private readonly string groupName;

        public TwOpcTagExtractor()
        {
            this.regex = new Regex(ConfigurationManager.AppSettings["opcTagExtractRegex"]);
            this.groupName = "opcTag";
        }

        public bool TryExtract(string twOpcTag, out string opcTag)
        {
            opcTag = null;
            var match = this.regex.Match(twOpcTag);
            if (match.Groups.Count != 2)
            {
                return false;
            }

            var groupNames = this.regex.GetGroupNames();
            if (!groupNames.Contains(this.groupName, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            try
            {
                var group = match.Groups[this.groupName];
                opcTag = group.Value;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
