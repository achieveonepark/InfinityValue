using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Achieve.InfinityValue
{
#if USE_NEWTONSOFT_JSON
    [JsonConverter(typeof(InfinityValueConverter))]
#endif
    public partial struct InfinityValue
        : IEquatable<InfinityValue>, IComparable<InfinityValue>
    {
        private Dictionary<int, long> _units;
        private bool _isNormalized;
        private string _cachedToString;

        /// <summary>
        /// 값이 비어있는가?
        /// </summary>
        public bool IsEmpty => _units == null;

        private static readonly List<string> unitNames = new List<string>
    {
        "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM",
        "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM",
        "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM",
        "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
    };

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

        private void Normalize()
        {
            if (_isNormalized) return;

            var newUnits = new Dictionary<int, long>();
            foreach (var unit in _units)
            {
                int index = unit.Key;
                long value = unit.Value;
                while (value >= 1000)
                {
                    long overflow = value / 1000;
                    value %= 1000;
                    newUnits[index++] = value;
                    value = overflow;
                }
                if (value > 0)
                {
                    newUnits[index] = value;
                }
            }
            _units = newUnits;
            _isNormalized = true;
            _cachedToString = null;
        }

        public static InfinityValue Parse(string input)
        {
            var matches = Regex.Matches(input, @"(\d+)([A-Z])");
            var values = new List<(int, long)>();

            foreach (Match match in matches)
            {
                long value = long.Parse(match.Groups[1].Value);
                int index = unitNames.IndexOf(match.Groups[2].Value);
                values.Add((index, value));
            }

            return new InfinityValue(values.ToArray());
        }

        public static InfinityValue FromLong(long number)
        {
            var values = new List<(int, long)>();

            for (int i = 0; number > 0; i++)
            {
                long value = number % 1000;
                if (value != 0)
                {
                    values.Add((i, value));
                }
                number /= 1000;
            }

            return new InfinityValue(values.ToArray());
        }

        public override string ToString()
        {
            if (_cachedToString != null) return _cachedToString;

            Normalize();

            if (_units.Count == 0)
                return "0";

            var highestUnit = _units.OrderByDescending(kvp => kvp.Key).FirstOrDefault();
            _cachedToString = $"{highestUnit.Value}{unitNames[highestUnit.Key]}";
            return _cachedToString;
        }

        private BigInteger ToBigInteger()
        {
            BigInteger result = 0;

            foreach (var kvp in _units)
            {
                result += new BigInteger(kvp.Value) * BigInteger.Pow(1000, kvp.Key);
            }

            return result;
        }

        public int CompareTo(InfinityValue other)
        {
            return ToBigInteger().CompareTo(other.ToBigInteger());
        }

        public bool Equals(InfinityValue other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is InfinityValue)
            {
                return Equals((InfinityValue)obj);
            }
            return false;
        }
    }
}