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

        public int getSplitHashLenght(string hash)
        {
            return int.Parse(hash.Substring(0, 4), System.Globalization.NumberStyles.HexNumber) % 5;
        }

        public int[] getTicketFromHash(string hash)
        {
            int[] result;
            ulong nummer = 0;
            string splitHash = "";

            int splitHashLenght = getSplitHashLenght(hash);

            foreach (char t in hash)
            {
                splitHash += t;
                if (splitHash.Length > (11 + splitHashLenght))
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

        public static double factorial(double n, int x = 1)
        {
            if (n > x)
                return n * factorial(n - 1, x);
            return n;
        }

        public static double binominal(double n, double k)
        {
            double tmp0, tmp1;
            if ((k == 0) || (n == k))
                return 1;
            else
            {
                tmp0 = binominal(n - 1, k);     // hier ist die rekursion ;)
                tmp1 = binominal(n - 1, k - 1); // und hier
                return tmp0 + tmp1;
            }
        }

        public string getArrayToString(int[] array)
        {
            string result = "[ ";
            foreach (int element in array)
                result += element + " ";
            result += "]";
            return result;
        }

        public void check()
        {
            int numbers = 5;
            int maxnumbers = 22;
            int stars = 2;
            int maxstars = 9;

            double totalPercentage = 0;

            double gain = 100;
            double totalGain = 0;


            Console.WriteLine("Combinations {0} !", factorial(maxnumbers, maxnumbers - numbers + 1) / factorial(numbers) * factorial(maxstars, maxstars - stars + 1) / factorial(stars));
            Console.WriteLine("POT {0} BTC", gain);

            double raiser = 1;

            for (int n = 5; n >= 0; n--)
            {
                double gainAllocate = (gain * (n)) / (n + raiser);
                gain -= gainAllocate;

                for (int s = 2; s >= 0; s--)
                {
                    double gainStepAllocate = (gainAllocate * (s + 2)) / (s + raiser + 2);
                    gainAllocate -= gainStepAllocate;
                    totalGain += gainStepAllocate;

                    raiser += 2;

                    double percentageNumbers = binominal(numbers, n) * binominal(maxnumbers - numbers, numbers - n) / binominal(maxnumbers, numbers);
                    double percentageStars = binominal(stars, s) * binominal(maxstars - stars, stars - s) / binominal(maxstars, stars);

                    double percentage = percentageNumbers * percentageStars;

                    totalPercentage += percentage;

                    string percentageString = String.Format("{0,10:0.00000}", (percentage * 100));

                    if (s > 0)
                        Console.WriteLine("{0} + {1} = {2} % - {3}", n, s, percentageString, String.Format("{0,15:0.00000000}", gainStepAllocate));
                    else
                        Console.WriteLine("{0}     = {1} % - {2}", n, percentageString, String.Format("{0,15:0.00000000}", gainStepAllocate));

                }
            }

            Console.WriteLine("TOTAL: ALL   = {0} %", totalPercentage * 100);
            Console.WriteLine("TOTAL: GAIN  = {0} BTC", totalGain);

            Console.ReadKey();
        }

    }

}


