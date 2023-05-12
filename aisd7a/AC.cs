using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace aisd7a
{
    internal class AC
    {
        public class ArithmeticCoding
        {
            private const int NumBits = 32;
            private const uint TopValue = 1u << NumBits;
            private const uint FirstQuarter = TopValue / 4u + 1u;
            private const uint Half = 2u * FirstQuarter;
            private const uint ThirdQuarter = 3u * FirstQuarter;

            private uint _low;
            private uint _high;
            private uint _value;
            private int _numUnderflow;

            private Dictionary<char, Range> _charRanges;


            public static Dictionary<char, int> GetCharCounts1(string text)
            {
                Dictionary<char, int> charCounts = new Dictionary<char, int>();
                foreach (char c in text)
                {
                    if (charCounts.ContainsKey(c))
                    {
                        charCounts[c]++;
                    }
                    else
                    {
                        charCounts[c] = 1;
                    }
                }
                return charCounts;
            }

            public static double Encode(string input, Dictionary<char, int> frequencies)
            {
                double low = 0.0;
                double high = 1.0;
                int totalChars = input.Length;

                foreach (char c in input)
                {
                    double range = high - low;
                    double cumFreqLow = frequencies.Where(x => x.Key < c).Sum(x => x.Value) / (double)totalChars;
                    double cumFreqHigh = frequencies.Where(x => x.Key <= c).Sum(x => x.Value) / (double)totalChars;

                    high = low + range * cumFreqHigh;
                    low = low + range * cumFreqLow;
                }

                return (high + low) / 2;
            }

            public static string Decode(double encoded, Dictionary<char, int> frequencies)
            {
                string decoded = "";
                int totalChars = frequencies.Values.Sum();
                double current = encoded;

                while (totalChars > 0)
                {
                    char foundChar = '\0';
                    double cumFreq = 0;

                    foreach (var pair in frequencies.OrderBy(x => x.Key))
                    {
                        double prevCumFreq = cumFreq;
                        cumFreq += pair.Value / (double)totalChars;

                        if (current >= prevCumFreq && current < cumFreq)
                        {
                            foundChar = pair.Key;
                            frequencies[foundChar]--;
                            totalChars--;

                            double low = prevCumFreq;
                            double high = cumFreq;
                            current = (current - low) / (high - low);

                            break;
                        }
                    }

                    if (foundChar != '\0')
                    {
                        decoded += foundChar;
                    }
                }

                return decoded;
            }
        }
    }
}
