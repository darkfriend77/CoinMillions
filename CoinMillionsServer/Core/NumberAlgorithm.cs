using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMillions.Core
{
    class NumberAlgorithm
    {
        Dictionary<ulong, int[]> ticketDictonary = new Dictionary<ulong, int[]>();

        public NumberAlgorithm()
        {
            generateNumbers();
        }

        public int[] getTicketFromHash(string hash)
        {
            int[] result;
            ulong nummer = 0;
            string splitHash = "";
            foreach (char t in hash)
            {
                splitHash += t;
                if (splitHash.Length > 15)
                {
                    nummer += ulong.Parse(splitHash, System.Globalization.NumberStyles.HexNumber);
                    splitHash = "";
                }
            }

            if (splitHash.Length > 0)
                nummer += ulong.Parse(splitHash, System.Globalization.NumberStyles.HexNumber);

            ulong key = nummer % (ulong)ticketDictonary.Count;

            //Console.WriteLine("Key: {0}", key);

            if (ticketDictonary.TryGetValue(key, out result))
                return result;
            else
                return null;
        }

        public void generateNumbers()
        {
            ulong count = 1;
            for (int a = 1; a <= 28 - 4; a++)
                for (int b = a + 1; b <= 28 - 3; b++)
                    for (int c = b + 1; c <= 28 - 2; c++)
                        for (int d = c + 1; d <= 28 - 1; d++)
                            for (int e = d + 1; e <= 28 - 0; e++)
                                for (int s1 = 1; s1 <= 5 - 1; s1++)
                                    for (int s2 = s1 + 1; s2 <= 5 - 0; s2++)
                                        ticketDictonary.Add(count++, new int[] { a, b, c, d, e, s1, s2 });

            Console.WriteLine("We have {0} solutions!", ticketDictonary.Count);
        }

        public int[] getTicketFromValue(double value)
        {
            if (value > 0.01 && value < 0.02)
            {
                ulong key = Convert.ToUInt64((value - 0.01) * Math.Pow(10, 8));
                return ticketDictonary[key];
            }
            return null;
        }

        private int[] getArrayOfIntegers(int maximum)
        {
            int[] arrayOfIntegers = new int[maximum];
            for (int i = 0; i < maximum; i++)
                arrayOfIntegers[i] = i + 1;
            return arrayOfIntegers;
        }

        public static long factorial(int n)
        {
            long tempResult = 1;
            for (int i = 1; i <= n; i++)
                tempResult *= i;
            return tempResult;
        }

        public string getArrayToString(int[] array)
        {
            string result = "[ ";
            foreach (int element in array)
                result += element + " ";
            result += "]";
            return result;
        }

    }

}


