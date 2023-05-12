using aisd7;
using Burrows_Wheeler_Data_Compression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Linq;
using static aisd7a.AC;

enum Operations
{
    HA = 0,
    RLE = 1,
    LZ78 = 2,
    BWT = 3,
    MTF = 4,
    AC = 5,
    PPM = 6
}

namespace aisd7
{
    class Program
    {
        static string run(Operations operation, string input, bool isFin = false)
        {
            string outputPath = @"C:\Users\krene\source\repos\aisd7a\aisd7a\output\";
            string filename = "";
            string encoded = "";
            byte[] encodedBytes = null;


            var sw = new Stopwatch();
            sw.Start();

            switch (operation)
            {
                case Operations.HA:
                    filename = "HA";

                    byte[] aaaaaa = HaCompression.Compress(Encoding.ASCII.GetBytes(input));

                    encoded += Encoding.ASCII.GetString(aaaaaa);

                    break;
                case Operations.RLE:
                    filename = "RLE";

                    encoded = RLE.Encode(input);

                    break;
                case Operations.LZ78:
                    filename = "LZ78";
                    byte[] bytes = Encoding.ASCII.GetBytes(input);
                    encodedBytes = LZ78.Encode(bytes);

                    encoded += Encoding.ASCII.GetString(encodedBytes);

                    break;
                case Operations.BWT:
                    filename = "BWT";

                    byte[] bw = BWCompression.Compress(Encoding.ASCII.GetBytes(input));

                    encoded += Encoding.ASCII.GetString(bw);

                    break;
                case Operations.MTF:
                    filename = "MTF";

                    byte[] mtf = BWCompression.Compress(Encoding.ASCII.GetBytes(input));

                    encoded += Encoding.ASCII.GetString(mtf);

                    break;
                case Operations.AC:
                    filename = "AC";

                    Dictionary<char, int> dict = ArithmeticCoding.GetCharCounts1(input);
                    double enc = ArithmeticCoding.Encode(input, dict);
                    encoded += enc;

                    break;
            }

            sw.Stop();

            string fullPath = outputPath + filename + @".txt";
            

            File.Create(fullPath).Close();
            
            File.WriteAllText(fullPath, encoded);
            

            long size = new FileInfo(outputPath + filename + @".txt").Length;
            

            Console.WriteLine("Compressing time: " + sw.ElapsedMilliseconds);
            Console.WriteLine("Size of file (kb): " + size / 1024);
            

            if (isFin)
            {
                string resultPath = outputPath + @"result.txt";

                File.Create(resultPath).Close();

                File.WriteAllText(resultPath, encoded);
                long resultsize = new FileInfo(resultPath).Length;

                Console.WriteLine("Size of result (kb): " + resultsize / 1024);
            }

            return encoded;
        }

        static void Main(string[] args)
        {
            //string input = File.ReadAllText(@"C:\Users\krene\source\repos\aisd7a\aisd7a\qqq.txt", Encoding.UTF8);
            string enwik8 = File.ReadAllText(@"C:\Users\krene\source\repos\aisd7a\aisd7a\enwik8", Encoding.UTF8);

            int length10 = (int)(enwik8.Length * 0.1);

            string input = enwik8.Substring(0, length10);

            string wikiPartPath = @"C:\Users\krene\source\repos\aisd7a\aisd7a\" + @"wikiPart.txt";
            File.Create(wikiPartPath).Close();
            File.WriteAllText(wikiPartPath, input);
            long resultsize = new FileInfo(wikiPartPath).Length;
            Console.WriteLine("Size of wiki part (kb): " + resultsize / 1024);

            //run(Operations.AC, input, true);


            //1
            //run(Operations.HA, input, true);

            //2
            //run(Operations.RLE, input, true);

            //3 
            //run(Operations.LZ78, input, true);

            // 4
            //string a = run(Operations.BWT, input);
            //string b = run(Operations.MTF, a);
            //string c = run(Operations.HA, b, true);

            // 5
            //string a = run(Operations.BWT, input);
            //string b = run(Operations.MTF, a);
            //string c = run(Operations.AC, b, true);

            // 6
            //string a = run(Operations.RLE, input);
            //string b = run(Operations.BWT, a);
            //string c = run(Operations.MTF, b);
            //string d = run(Operations.RLE, c);
            //string e = run(Operations.HA, d, true);

            // 7
            string a = run(Operations.RLE, input);
            string b = run(Operations.BWT, a);
            string c = run(Operations.MTF, b);
            string d = run(Operations.RLE, c);
            string e = run(Operations.AC, d, true);
        }
    }
}