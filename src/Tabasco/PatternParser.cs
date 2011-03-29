
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tabasco
{
    public class PatternParser
    {
        private readonly string _pattern;
        private readonly List<string> _keys;
        const string CatchAllKey = ":star";

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

        public dynamic Match(string url)
        {
            var matches = new Dictionary<string, dynamic>();

            var regex = new Regex(_pattern, RegexOptions.IgnoreCase);

            var groups = regex.Match(url).Groups;

            if (groups.Count > 1)
            {
                for (var groupIndex = 1; groupIndex < groups.Count; groupIndex++)
                {
                    var key = _keys[0] == CatchAllKey ? groupIndex.ToString() : _keys[groupIndex - 1];

                    matches.Add(key, NRack.Utils.Unescape(groups[groupIndex].Value));
                }
            }

            if (_keys[0] == CatchAllKey)
            {
                return new Dictionary<string, dynamic> { { CatchAllKey, matches.Values.ToArray() } };
            }

            return matches;
        }

        private string Parse(string pattern)
        {
            var line = pattern.Split(' ');

            Method = line[0];
            pattern = line[1];

            var regex = new Regex(@"(:(\w+))|\*", RegexOptions.IgnoreCase);
            var matches = regex.Matches(pattern);


            var parsedPattern = pattern;

            for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++)
            {
                var value = matches[matchIndex].Value;

                if (value == "*" && !_keys.Contains(CatchAllKey))
                {
                    _keys.Add(CatchAllKey);
                    parsedPattern = regex.Replace(pattern, "(.*)");
                    break;
                }

                _keys.Add(matches[matchIndex].Value);
            }

            if (!_keys.Contains(CatchAllKey))
            {
                parsedPattern = regex.Replace(parsedPattern, "([^/?#]+)");
            }

            return parsedPattern;
        }
    }
}