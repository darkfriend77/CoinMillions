using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace CoinMillionsServer.Core
{
    class Lottery
    {
        const int NUMBERS = 5;
        const int MAXNUMBERS = 22;
        const int STARS = 2;
        const int MAXSTARS = 9;

        private List<Finding> findings = new List<Finding>();

        private Dictionary<ulong, int[]> ticketDictonary = new Dictionary<ulong, int[]>();

        private CoinMillionsModelContainer database;

        public Lottery(CoinMillionsModelContainer database)
        {
            this.database = database;

            generateShapes();
            generateNumbers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Finding> getShapes()
        {
            return findings;
        }

        /// <summary>
        /// 
        /// </summary>
        private void generateShapes()
        {
            double gain = 1;
            double raiser = 1;
            double totalGain = 0;
            double gainStepAllocate = 0;
            double gainAllocate = 0;

            for (int n = 5; n >= 0; n--)
            {
                gainAllocate = (gain * (n)) / (n + raiser);
                gain -= gainAllocate;

                for (int s = 2; s >= 0; s--)
                {
                    gainStepAllocate = (gainAllocate * (s + 2)) / (s + raiser + 2);
                    gainAllocate -= gainStepAllocate;
                    totalGain += gainStepAllocate;

                    raiser += 2;

                    findings.Add(new Finding()
                    {
                        Numbers = n,
                        Stars = s,
                        Probability = SpecialFunctions.Binomial(NUMBERS, n) 
                                    * SpecialFunctions.Binomial(MAXNUMBERS - NUMBERS, NUMBERS - n) 
                                    / SpecialFunctions.Binomial(MAXNUMBERS, NUMBERS) 
                                    * SpecialFunctions.Binomial(STARS, s) 
                                    * SpecialFunctions.Binomial(MAXSTARS - STARS, STARS - s) 
                                    / SpecialFunctions.Binomial(MAXSTARS, STARS),
                        Gain = gainStepAllocate
                    });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public int getSplitHashLenght(string hash)
        {
            return int.Parse(hash.Substring(0, 4), System.Globalization.NumberStyles.HexNumber) % 5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public int[] getTicketFromHash(string hash)
        {
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

            return ticketDictonary[key];
        }

        /// <summary>
        /// 
        /// </summary>
        private void generateNumbers()
        {
            ulong count = 1;
            
            // numbers
            for (int a = 1; a < MAXNUMBERS - NUMBERS + 2; a++)
                for (int b = a + 1; b < MAXNUMBERS - NUMBERS + 3; b++)
                    for (int c = b + 1; c < MAXNUMBERS - NUMBERS + 4; c++)
                        for (int d = c + 1; d < MAXNUMBERS - NUMBERS + 5; d++)
                            for (int e = d + 1; e < MAXNUMBERS - NUMBERS + 6; e++)

                                // stars
                                for (int s = 1; s < MAXSTARS - STARS + 4; s++)
                                    for (int t = s + 1; t < MAXSTARS - STARS + 3; t++)
                                        ticketDictonary.Add(count++, new int[] { a, b, c, d, e, s, t });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getTicketCount()
        {
            return ticketDictonary.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int[] getTicketFromValue(double value)
        {
            if (value > 0.01 && value < 0.02)
            {
                ulong key = Convert.ToUInt64((value - 0.01) * Math.Pow(10, 8));
                return ticketDictonary[key];
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximum"></param>
        /// <returns></returns>
        private int[] getArrayOfIntegers(int maximum)
        {
            int[] arrayOfIntegers = new int[maximum];
            for (int i = 0; i < maximum; i++)
                arrayOfIntegers[i] = i + 1;
            return arrayOfIntegers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public string getArrayToString(int[] array)
        {
            string result = "[ ";
            foreach (int element in array)
                result += element + " ";
            result += "]";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketA"></param>
        /// <param name="ticketB"></param>
        /// <returns></returns>
        public Finding compareTicket(int[] ticketA, int[] ticketB)
        {
            int[] numbersA = new int[NUMBERS];
            int[] numbersB = new int[NUMBERS];

            int[] starsA = new int[STARS];
            int[] starsB = new int[STARS];

            Array.Copy(ticketA, 0, numbersA, 0, NUMBERS);
            Array.Copy(ticketB, 0, numbersB, 0, NUMBERS);
            Array.Copy(ticketA, 5, starsA, 0, STARS);
            Array.Copy(ticketB, 5, starsB, 0, STARS);

            return getFinding(new int[] { compareArray(numbersA, numbersB), compareArray(starsA, starsB) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketA"></param>
        /// <param name="ticketB"></param>
        /// <returns></returns>
        //public int[] compareTicket(int[] ticketA, int[] ticketB)
        //{
        //    int[] numbersA = new int[NUMBERS];
        //    int[] numbersB = new int[NUMBERS];

        //    int[] starsA = new int[STARS];
        //    int[] starsB = new int[STARS];

        //    Array.Copy(ticketA, 0, numbersA, 0, NUMBERS);
        //    Array.Copy(ticketB, 0, numbersB, 0, NUMBERS);
        //    Array.Copy(ticketA, 5, starsA, 0, STARS);
        //    Array.Copy(ticketB, 5, starsB, 0, STARS);

        //    return new int[] { compareArray(numbersA, numbersB), compareArray(starsA, starsB) };
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbersA"></param>
        /// <param name="numbersB"></param>
        /// <returns></returns>
        private int compareArray(int[] numbersA, int[] numbersB)
        {
            int count = 0;
            foreach (var numberA in numbersA)
                foreach (var numberB in numbersB)
                    if (numberA == numberB)
                        count++;
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="stars"></param>
        /// <returns></returns>
        public Finding getFinding(int[] numbersStars)
        {
            return findings.Where(s => s.Numbers == numbersStars[0] && s.Stars == numbersStars[1]).First();
        }

    }

    //class Finding
    //{
    //    public int Stars { get; set; }
    //    public int Numbers { get; set; }
    //    public double Probability { get; set; }
    //    public double Gain { get; set; }

    //    public string printString()
    //    {
    //        return string.Format("N: {1}; S: {0}; P: {2}; G: {3}", Numbers, Stars, string.Format("{0,10:0.00000 %}", Probability), string.Format("{0,8:0.000000}", Gain));
    //    }
    //}

}
