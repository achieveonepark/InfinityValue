using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Achieve.InfinityValue
{
    public partial struct InfinityValue
    {
        public override int GetHashCode() => ToBigInteger().GetHashCode();
        public static bool operator >(InfinityValue a, InfinityValue b) => a.CompareTo(b) > 0;
        public static bool operator <(InfinityValue a, InfinityValue b) => a.CompareTo(b) < 0;
        public static bool operator >=(InfinityValue a, InfinityValue b) => a.CompareTo(b) >= 0;
        public static bool operator <=(InfinityValue a, InfinityValue b) => a.CompareTo(b) <= 0;
        public static bool operator ==(InfinityValue a, InfinityValue b) => a.Equals(b);
        public static bool operator !=(InfinityValue a, InfinityValue b) => !a.Equals(b);
        public static bool operator >(InfinityValue a, int b) => a > new InfinityValue(b);
        public static bool operator <(InfinityValue a, int b) => a < new InfinityValue(b);
        public static bool operator >=(InfinityValue a, int b) => a >= new InfinityValue(b);
        public static bool operator <=(InfinityValue a, int b) => a <= new InfinityValue(b);
        public static bool operator ==(InfinityValue a, int b) => a == new InfinityValue(b);
        public static bool operator !=(InfinityValue a, int b) => a != new InfinityValue(b);
        public static bool operator >(int a, InfinityValue b) => new InfinityValue(a) > b;
        public static bool operator <(int a, InfinityValue b) => new InfinityValue(a) < b;
        public static bool operator >=(int a, InfinityValue b) => new InfinityValue(a) >= b;
        public static bool operator <=(int a, InfinityValue b) => new InfinityValue(a) <= b;
        public static bool operator ==(int a, InfinityValue b) => new InfinityValue(a) == b;
        public static bool operator !=(int a, InfinityValue b) => new InfinityValue(a) != b;
        public static bool operator >(InfinityValue a, long b) => a > new InfinityValue(b);
        public static bool operator <(InfinityValue a, long b) => a < new InfinityValue(b);
        public static bool operator >=(InfinityValue a, long b) => a >= new InfinityValue(b);
        public static bool operator <=(InfinityValue a, long b) => a <= new InfinityValue(b);
        public static bool operator ==(InfinityValue a, long b) => a == new InfinityValue(b);
        public static bool operator !=(InfinityValue a, long b) => a != new InfinityValue(b);
        public static bool operator >(long a, InfinityValue b) => new InfinityValue(a) > b;
        public static bool operator <(long a, InfinityValue b) => new InfinityValue(a) < b;
        public static bool operator >=(long a, InfinityValue b) => new InfinityValue(a) >= b;
        public static bool operator <=(long a, InfinityValue b) => new InfinityValue(a) <= b;
        public static bool operator ==(long a, InfinityValue b) => new InfinityValue(a) == b;
        public static bool operator !=(long a, InfinityValue b) => new InfinityValue(a) != b;
        public static bool operator >(InfinityValue a, BigInteger b) => a.ToBigInteger() > b;
        public static bool operator <(InfinityValue a, BigInteger b) => a.ToBigInteger() < b;
        public static bool operator >=(InfinityValue a, BigInteger b) => a.ToBigInteger() >= b;
        public static bool operator <=(InfinityValue a, BigInteger b) => a.ToBigInteger() <= b;
        public static bool operator ==(InfinityValue a, BigInteger b) => a.ToBigInteger() == b;
        public static bool operator !=(InfinityValue a, BigInteger b) => a.ToBigInteger() != b;
        public static bool operator >(BigInteger a, InfinityValue b) => a > b.ToBigInteger();
        public static bool operator <(BigInteger a, InfinityValue b) => a < b.ToBigInteger();
        public static bool operator >=(BigInteger a, InfinityValue b) => a >= b.ToBigInteger();
        public static bool operator <=(BigInteger a, InfinityValue b) => a <= b.ToBigInteger();
        public static bool operator ==(BigInteger a, InfinityValue b) => a == b.ToBigInteger();
        public static bool operator !=(BigInteger a, InfinityValue b) => a != b.ToBigInteger();
        public static InfinityValue operator +(InfinityValue a, int b) => a + new InfinityValue(b);
        public static InfinityValue operator +(InfinityValue a, long b) => a + new InfinityValue(b);
        public static InfinityValue operator +(InfinityValue a, BigInteger b) => a + FromBigInteger(b);
        public static InfinityValue operator +(int a, InfinityValue b) => new InfinityValue(a) + b;
        public static InfinityValue operator +(long a, InfinityValue b) => new InfinityValue(a) + b;
        public static InfinityValue operator -(InfinityValue a, int b) => a - new InfinityValue(b);
        public static InfinityValue operator -(InfinityValue a, long b) => a - new InfinityValue(b);
        public static InfinityValue operator -(InfinityValue a, BigInteger b) => a - FromBigInteger(b);
        public static InfinityValue operator -(int a, InfinityValue b) => new InfinityValue(a) - b;
        public static InfinityValue operator -(long a, InfinityValue b) => new InfinityValue(a) - b;
        public static InfinityValue operator *(InfinityValue a, float b) => Multiply(a, b);
        public static InfinityValue operator *(InfinityValue a, long b) => Multiply(a, b);
        public static InfinityValue operator *(InfinityValue a, int b) => Multiply(a, b);
        public static implicit operator InfinityValue(int value) => new InfinityValue(value);
        public static implicit operator InfinityValue(long value) => new InfinityValue(value);
        public static implicit operator InfinityValue(BigInteger value) => FromBigInteger(value);
        public static implicit operator InfinityValue(string value) => new InfinityValue(value);
        public static explicit operator float(InfinityValue value) => (float)value.ToBigInteger();

        public static InfinityValue operator +(InfinityValue a, InfinityValue b)
        {
            var result = new Dictionary<int, long>(a._units);

            foreach (var kvp in b._units)
            {
                if (result.ContainsKey(kvp.Key))
                {
                    result[kvp.Key] += kvp.Value;
                }
                else
                {
                    result[kvp.Key] = kvp.Value;
                }
            }

            return new InfinityValue(result.ToArray());
        }

        public static InfinityValue operator -(InfinityValue a, InfinityValue b)
        {
            var result = new Dictionary<int, long>(a._units);

            foreach (var kvp in b._units)
            {
                if (result.ContainsKey(kvp.Key))
                {
                    result[kvp.Key] -= kvp.Value;
                }
                else
                {
                    result[kvp.Key] = -kvp.Value;
                }
            }

            for (int i = 0; i < unitNames.Count - 1; i++)
            {
                if (result.ContainsKey(i) && result[i] < 0)
                {
                    long borrow = (Math.Abs(result[i]) + 999) / 1000;
                    result[i] += borrow * 1000;
                    if (result.ContainsKey(i + 1))
                    {
                        result[i + 1] -= borrow;
                    }
                    else
                    {
                        result[i + 1] = -borrow;
                    }
                }
            }

            return new InfinityValue(result.Where(kvp => kvp.Value != 0).ToArray());
        }

        public static InfinityValue operator *(InfinityValue a, InfinityValue b)
        {
            var result = new Dictionary<int, long>();

            foreach (var aKvp in a._units)
            {
                foreach (var bKvp in b._units)
                {
                    int index = aKvp.Key + bKvp.Key;
                    long value = aKvp.Value * bKvp.Value;

                    if (result.ContainsKey(index))
                    {
                        result[index] += value;
                    }
                    else
                    {
                        result[index] = value;
                    }
                }
            }

            return new InfinityValue(result.ToArray());
        }

        public static InfinityValue operator /(InfinityValue a, InfinityValue b)
        {
            var result = new Dictionary<int, long>(a._units);

            foreach (var kvp in b._units)
            {
                if (kvp.Value == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero.");
                }

                if (result.ContainsKey(kvp.Key))
                {
                    result[kvp.Key] /= kvp.Value;
                }
                else
                {
                    result[kvp.Key] = 0;
                }
            }

            return new InfinityValue(result.Where(kvp => kvp.Value != 0).ToArray());
        }
        public static InfinityValue FromBigInteger(BigInteger number)
        {
            var values = new List<(int, long)>();

            for (int i = 0; number > 0; i++)
            {
                long value = (long)(number % 1000);
                if (value != 0)
                {
                    values.Add((i, value));
                }
                number /= 1000;
            }

            return new InfinityValue(values.ToArray());
        }

        private static InfinityValue Multiply(InfinityValue a, double b)
        {
            var result = new Dictionary<int, long>();
            foreach (var kvp in a._units)
            {
                double rawValue = kvp.Value * b;
                long value = (long)rawValue;
                int index = kvp.Key;

                while (rawValue >= 1000)
                {
                    long overflow = (long)(rawValue / 1000);
                    result[index++] = (long)(rawValue % 1000);
                    rawValue = overflow;
                }

                if (rawValue != 0)
                {
                    result[index] = (long)rawValue;
                }
            }

            return new InfinityValue(result.ToArray());
        }
    }
}