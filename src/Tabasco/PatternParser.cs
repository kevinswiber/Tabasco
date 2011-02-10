
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tabasco
{
    public class PatternParser
    {
        private readonly string _pattern;
        private readonly List<string> _keys;

        public PatternParser(string pattern)
        {
            _keys = new List<string>();
            _pattern = Parse(pattern);
        }

        public string Method { get; private set; }

        public bool IsMatch(string url)
        {
            var line = url.Split(' ');

            if (line[0] != Method)
            {
                return false;
            }

            if (_pattern == "/")
            {
                return _pattern == line[1];
            }

            var regex = new Regex(_pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(line[1]);
        }

        public IDictionary<string, string> Match(string url)
        {
            var matches = new Dictionary<string, string>();

            var regex = new Regex(_pattern, RegexOptions.IgnoreCase);

            var groups = regex.Match(url).Groups;

            if (groups.Count > 1)
            {
                for (var groupIndex = 1; groupIndex < groups.Count; groupIndex++)
                {
                    matches.Add(_keys[groupIndex - 1], groups[groupIndex].Value);
                }
            }

            return matches;
        }

        private string Parse(string pattern)
        {
            var line = pattern.Split(' ');

            Method = line[0];
            pattern = line[1];

            var regex = new Regex(@":(\w+)", RegexOptions.IgnoreCase);

            var matches = regex.Matches(pattern);

            for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++)
            {
                _keys.Add(matches[matchIndex].Value);
            }
            return regex.Replace(pattern, "([^/?#]+)");
        }
    }
}