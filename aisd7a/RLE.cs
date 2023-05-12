using System;
using System.Text;

class RLE_Compress
{
    public static string Compress(string input)
    {
        StringBuilder output = new StringBuilder();
        int count = 1;
        char currentChar = input[0];

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == currentChar)
            {
                count++;
            }
            else
            {
                output.Append(count);
                output.Append(currentChar);
                count = 1;
                currentChar = input[i];
            }
        }

        output.Append(count);
        output.Append(currentChar);

        return output.ToString();
    }

    public static string Decompress(string input)
    {
        StringBuilder output = new StringBuilder();
        int count = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsDigit(input[i]))
            {
                count = count * 10 + (input[i] - '0');
            }
            else
            {
                for (int j = 0; j < count; j++)
                {
                    output.Append(input[i]);
                }
                count = 0;
            }
        }

        return output.ToString();
    }
}

namespace aisd7
{
    internal class RLE
    {
        public static string Encode(string input)
        {
            string compressed = RLE_Compress.Compress(input);

            return compressed;
        }
    }
}
