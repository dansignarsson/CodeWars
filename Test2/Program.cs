using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeWars
{

    class Program
    {
        private static Dictionary<string, string> _morseAlphabetDictionary;

        static void Main(string[] args)
        {
            //Morse // CodeWars web interface supplied an API for this dictionary
            InitializeDictionary();


            string decodedString = MorseDecoder.Decoder(".... . -.--   .--- ..- -.. .");
            Console.WriteLine(decodedString);


            //Tribonacci
            int n = 22;
            double[] tribo = new double[] { 0, 3, 16 };
            double[] result = Tribonacci(tribo, n);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }


            //ValidatePin
            //string[] testCases = new string[] { "1234-+", "1234\n", "573012    ", "120412", "0000" };
            
            string a = "1233-+";
            string b = "1234\n";
            string c = "573012   ";
            string d = "120412";
            string e = "0000";

            bool resA = ValidatePin(a);
            bool resB = ValidatePin(b);
            bool resC = ValidatePin(c);
            bool resD = ValidatePin(d);
            bool resE = ValidatePin(e);

            Console.WriteLine(resA);
            Console.WriteLine(resB);
            Console.WriteLine(resC);
            Console.WriteLine(resD);
            Console.WriteLine(resE);


            //Disemvowel

            string resultB = Disemvowel("This website is for losers LOL!");
            Console.WriteLine(resultB);


            //Exes and Ohs

            //(true, XO("xo"))
            //(true, XO("xxOo"))
            //(false, XO("xxxm"))
            //(false, XO("Oo"))
            //(false, XO("ooom"))
            var inputA = "xo";
            var inputB = "xXOo";
            var inputC = "xoomm";

            bool resultBoolA = XO(inputA);
            bool resultBoolB = XO(inputB);
            bool resultBoolC = XO(inputC);
            Console.WriteLine(resultBoolA);
            Console.WriteLine(resultBoolB);
            Console.WriteLine(resultBoolC);


            // Sum Of Numbers(from A to B)
            int sumA = -1;
            int sumB = -5;


            int sum = GetSum(sumA, sumB);
            Console.WriteLine(sum);

            //Square Every Digit
            int squareMe = 9999;
            int squaredMe = SquareDigits(squareMe);

            Console.WriteLine(squaredMe);




        }

        public static int SquareDigits(int n)
        {
            int[] digits = n.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();

            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = digits[i] * digits[i];
            }

            int result = 0;

            Int32.TryParse(string.Join("", digits), out result);

            return result;


        }


        private static int GetSum(int a, int b)
        {
            int sum = 0;

            if (a == b)
                return a;

            if (a > b)
            {
                for (; b <= a; b++)
                {
                    sum += b;
                }

                return sum;
            }

            else 
            {
                for (; a <= b; a++)
                {
                    sum += a;
                }

                return sum;
            }
        }

        public static bool XO(string input)
        {

            int oCount = 0;
            int xCount = 0;

            foreach (char c in input)
            {
                if (Char.ToLower(c) == 'x') xCount++;
                else if (Char.ToLower(c) == 'o') oCount++;
            }

                return xCount == oCount;

            //Cool 1Line solution
            // return input.ToLower().Count(i => i == 'x') == input.ToLower().Count(i => i == 'o');


        }



        //
        public static string Disemvowel(string str)
        {
            //A, I, E, O, U

            //    for (int i = 0; i < a.Length; i++)
            //    {
            //    a = a.Replace("A", "");
            //    a = a.Replace("E", "");
            //    a = a.Replace("I", "");
            //    a = a.Replace("O", "");
            //    a = a.Replace("o", "");
            //    a = a.Replace("U", "");
            //      }
            //return a; 


            return Regex.Replace(str, "[aeiouAEIOU]", "");
        }





        public static bool ValidatePin(string pin)
        {
            return (pin.Length == 4 || pin.Length == 6) && pin.All(n => char.IsDigit(n));
        }


        //Testing for signature: 12, 2, 8 and n: 36
        //Testing for signature: 0, 3, 16 and n: 1
        static double[] Tribonacci(double[] signature, int n)
        {
            double[] dp = new double[n];

            // hackonacci me
            if (n == 0)
                return Array.Empty<double>();


            if (n == 1)
            {
                signature[0] = dp[0];
                return dp;
            }

            if (n == 2)
            {
                dp[0] = signature[0];
                dp[1] = signature[1];

                return dp;
            }

            else
            {
                int h = 0;

                foreach (var item in signature)
                {
                    dp[h] = signature[h];
                    h++;
                }

                for (int j = 3; j < n; j++)
                {
                    dp[j] = dp[j - 1] +
                            dp[j - 2] +
                            dp[j - 3];
                }
                return dp;
            }
        }

        //Top Rated solution
        public double[] TribonacciBestPractice(double[] s, int n)
        {
            double[] res = new double[n];
            Array.Copy(s, res, Math.Min(3, n));

            for (int i = 3; i < n; i++)
                res[i] = res[i - 3] + res[i - 2] + res[i - 1];

            return n == 0 ? new double[] { 0 } : res;
        }



        //string input = ".... . -.--   .--- ..- -.. .";
        //string expected = "HEY JUDE";
        class MorseDecoder
        {
            public static string Decoder(string input)
            {

                string[] separateWords = input.Split("   ");

                StringBuilder sb = new StringBuilder();
                int count = 0;

                foreach (var word in separateWords)
                {
                    string[] separateChars = word.Split(" ");

                    foreach (var separateChar in separateChars)
                    {
                        if (_morseAlphabetDictionary.ContainsKey(separateChar))
                        {
                            sb.Append(_morseAlphabetDictionary[separateChar]);
                        }
                    }
                    if (separateWords.Length > 1 && separateWords.Length != count && word != string.Empty)
                        sb.Append(" ");
                }

                return sb.ToString();
            }
        }

        private static void InitializeDictionary()
        {
            _morseAlphabetDictionary = new Dictionary<string, string>()
                                   {
                                       {".-", "A"},
                                       {"-...", "B"},
                                       {"-.-.", "C"},
                                       {"-..", "D"},
                                       {".", "E"},
                                       {"..-.", "F"},
                                       {"--.", "G"},
                                       {"....", "H"},
                                       {"..", "I"},
                                       {".---", "J"},
                                       {"-.-", "K"},
                                       {".-..", "L"},
                                       {"--", "M"},
                                       {"-.", "N" },
                                       {"---", "O"},
                                       {".--.", "P"},
                                       {"--.-", "Q"},
                                       {".-.", "R"},
                                       { "...", "S"},
                                       {"-", "T"},
                                       {"..-", "U"},
                                       {"...-", "V"},
                                       {".--", "W"},
                                       {"-..-", "X"},
                                       {"-.--", "Y"},
                                       {"--..", "Z"},
                                       {"-----", "0"},
                                       {".----", "1"},
                                       {"..---", "2"},
                                       {"...--", "3"},
                                       {"....-", "4"},
                                       {".....", "5"},
                                       {"-....", "6"},
                                       {"--...", "7"},
                                       {"---..", "8"},
                                       {"----.", "9"}
                                   };
        }

    }
}
