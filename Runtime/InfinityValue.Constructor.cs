using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        public InfinityValue(KeyValuePair<int, long>[] keyValuePairs)
        {
            _units = keyValuePairs.Where(kvp => kvp.Value != 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            _isNormalized = false;
            _cachedToString = null;
        }

        public InfinityValue(params (int index, long value)[] values)
        {
            _units = new Dictionary<int, long>();
            _isNormalized = false;
            _cachedToString = null;

            foreach (var (index, value) in values)
            {
                if (value != 0)
                {
                    _units[index] = value;
                }
            }

            Normalize();
        }

        public InfinityValue(string input)
        {
            _units = new Dictionary<int, long>();
            var matches = Regex.Matches(input, @"(\d+)([A-Z]+)");
            foreach (Match match in matches)
            {
                long value = long.Parse(match.Groups[1].Value);
                int index = unitNames.IndexOf(match.Groups[2].Value);
                if (index >= 0)
                {
                    _units[index] = value;
                }
            }
            _isNormalized = false;
            _cachedToString = null;
        }

        public InfinityValue(long number)
        {
            _units = new Dictionary<int, long>();
            this = FromLong(number);
        }
        
        public InfinityValue(float a)
        {
            _units = new Dictionary<int, long>();
            long longValue = (long)a;
            this = FromLong(longValue);
        }

        public static implicit operator InfinityValue(int value) => new InfinityValue(value);
        public static implicit operator InfinityValue(long value) => new InfinityValue(value);
        public static implicit operator InfinityValue(BigInteger value) => FromBigInteger(value);
        public static implicit operator InfinityValue(string value) => new InfinityValue(value);
        public static implicit operator InfinityValue(float value) => new InfinityValue(value);
    }
}