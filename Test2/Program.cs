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

            bool resA = ValidatePin("1233-+");
            bool resB = ValidatePin("1234\n");
            bool resC = ValidatePin("573012   ");
            bool resD = ValidatePin("120412");
            bool resE = ValidatePin("0000");

            Console.WriteLine(resA);
            Console.WriteLine(resB);
            Console.WriteLine(resC);
            Console.WriteLine(resD);
            Console.WriteLine(resE);


            //Disemvowel

            string resultA = Disemvowel("This website is for losers LOL!");
            string resultB = Disemvowel("Your mother is a terrible weight watcher LOL!");
            Console.WriteLine(resultA);
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

            //DuplicateCount

            int duplicateCount = DuplicateCount("AaBbCc44a");
            Console.WriteLine("Expected: 4 Count: " + duplicateCount);



            //ListFilterer

            //var list = new List<object>() { 1, 2, "a", "b" };
            //var resultR = GetIntegersFromList(list);
            //foreach (var item in resultR)
            //{
            //    Console.WriteLine(item);
            //}


            //IQTest..
            Console.WriteLine(Test("2 4 7 8 10"));

        }



        public static int Test(string numbers)
        {
            string[] test = numbers.Split(' ');
            int[] testA = new int[test.Length];

            for (int i = 0; i < test.Length; i++)
            {
                testA[i] = Int32.Parse(test[i]);
            }

            int evenCount = 0;
            int unevenCount = 0;

            int unevenPlacement = 0;
            int evenPlacement = 0;

            for (int i = 0; i < testA.Length; i++)
            {
                if (testA[i] % 2 > 0)
                {
                    unevenCount++;
                    unevenPlacement = i;
                }
                else
                {
                    evenCount++;
                    evenPlacement = i;
                }
            }

            if (evenCount < unevenCount)
            {
                return evenPlacement +1;
            }


            else
                return unevenPlacement +1;
        }

        //Should have done this...
        public static int TestA(string numbers)
        {
            var ints = numbers.Split(' ').Select(int.Parse).ToList();
            var unique = ints.GroupBy(n => n % 2).OrderBy(c => c.Count()).First().First();
            return ints.FindIndex(c => c == unique) + 1;
        }




        public static int DetermineEven(Predicate<int> predicate)
        {

            return 0;
        }

        //    return c;
        //}





        public static int DuplicateCount(string str)
        {
            return str.ToUpper().GroupBy(c => c).Count(c => c.Count() > 1);
        }

        public static int SquareDigits(int n)
        {
            int[] digits = n.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();

            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = digits[i] * digits[i];
            }

            Int32.TryParse(string.Join("", digits), out int result);

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
