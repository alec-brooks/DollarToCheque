using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DollarToCheque
{
    public class Program
    {
        //Numbers represented as text for writing cheque
        static readonly List<string> onesList = new List<string>
         { 
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE",
            "SIX", "SEVEN", "EIGHT", "NINE"
         };

        static readonly List<string> teensList = new List<string>
         { 
            "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN",
            "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
         };

        static readonly List<string> tensList = new List<string>
         { 
            "","TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY",
            "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
         };
        static readonly List<string> exponentList = new List<string>
         { 
            "QUADRILLION", "TRILLION", "BILLION", "MILLION", "THOUSAND"
         };

        //Common words used on cheques
        const string AND = "AND ";
        const string HUNDRED = "HUNDRED";
        const string SPACE = " ";
        const string DASH = "-";
        const string DOLLARS = "DOLLARS ";
        const string CENTS = "CENTS";
        const string ZERO = "ZERO ";
        const string ZEROES = "000";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Please include a valid dollar value.");
                System.Console.WriteLine("Eg. 123.45");
                Console.ReadKey();
                return;
            }

            Program program = new Program();

            if (program.validate(args[0]))
            {
                Console.WriteLine(program.convertDollarToText(args[0]));
                Console.ReadKey();
                return;
            }
            else
            {
                System.Console.WriteLine("Invalid Number");
                System.Console.WriteLine("Please include a valid dollar value.");
                System.Console.WriteLine("Eg. 123.45");
                return;
            }

        }

        public bool validate(string input)
        {

            Regex validate = new Regex(@"^(\d+\.\d{2}$|\d+$)");

            if (validate.IsMatch(input) && Convert.ToDecimal(input) < 999999999999999999 && Convert.ToDecimal(input) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*  Input:  Valid dollar string. Eg. 123.45 or 123
         *  Output: Cheque String
         *  Method: Seperates dollars and cents, splits dollar value into groups of 3,
         *          converts each block to text while adding appropriate number values,
         *          appends cent value at the end          
         */
        public string convertDollarToText(string dollarDigit)
        {
            string chequeOutput = "";
            int dollarBlockSize = 3;
            string dollarValue;
            string centValue = "";

            //False if no cent value, or cent value is .00
            bool centBool;

            // Seperate dollar and cent values
            if (dollarDigit.Contains("."))
            {
                string[] dollarCents = dollarDigit.Split('.');
                dollarValue = dollarCents[0];
                centValue = dollarCents[1];

                if (centValue == "00")
                {
                    centBool = false;
                }
                else
                {
                    centValue = TwoToText(centValue);
                    centBool = true;
                }
            }
            else
            {
                dollarValue = dollarDigit;
                centBool = false;
            }

            //Splits string into blocks
            List<string> dollarList = splitStringIntoBlocks(dollarValue, dollarBlockSize);

            //Converts each block to text
            for (int i = 0; i < dollarList.Count; i++)
            {
                if (dollarList[i].Length == 1)
                {
                    chequeOutput += OneToText(dollarList[i]) + SPACE;
                }
                else if (dollarList[i].Length == 2)
                {
                    chequeOutput += TwoToText(dollarList[i]) + SPACE;
                }
                else if (dollarList[i].Length == 3)
                {
                    //Adds an "AND" before the true tens collumn of the dollar amount
                    if ((i < dollarList.Count))
                    {
                        chequeOutput += ThreeToText(dollarList[i], false) + SPACE;
                    }
                    else
                    {
                        chequeOutput += ThreeToText(dollarList[i], true) + SPACE;
                    }
                }

                //Adds indication of scale (thousand, million, billion etc)
                if ((i != dollarList.Count - 1) && (dollarList[i] != ZEROES))
                {
                    chequeOutput += exponentList[i + exponentList.Count - dollarList.Count + 1] + SPACE;
                }
            }
            //Adds appropriate cent value
            if (centBool)
            {
                Console.WriteLine(chequeOutput);
                if (String.IsNullOrWhiteSpace(chequeOutput)){
                    chequeOutput = ZERO + DOLLARS + AND + centValue + SPACE + CENTS;
                }else{
                chequeOutput += DOLLARS + AND + centValue + SPACE + CENTS;
                }
            }
            else
            {
                chequeOutput += DOLLARS + AND + ZERO + CENTS;
            }
            return chequeOutput;
        }

        /*  Input:  a string of numbers one character long
         *  Output: a string representing the input in text form
         *  Method: retrieves text from onesList
         */
        public string OneToText(string s)
        {
            return onesList[Convert.ToInt16(s)];
        }

        /*  Input:  a string of numbers two characters long
         *  Output: a string representing the input in text form
         *  Method: if teen, retrieves text from teensList, otherwise
         *          combines tensList word with onesList word
         */
        public string TwoToText(string s)
        {
            int n = Convert.ToInt16(s);
            if (s == "00" || s == "0")
            {
                return "";
            }
            if ((n > 10) && (n < 20))
            {
                return teensList[n - 11];
            }
            else
            {
                if (n / 10 == 0)
                {
                    return OneToText(Convert.ToString(n % 10));
                }
                else if (n % 10 == 0)
                {
                    return tensList[(n / 10)];
                }
                else
                {
                    return tensList[(n / 10)] + DASH + OneToText(Convert.ToString(n % 10));
                }

            }
        }

        /*  Input:  a string of numbers three characters long
         *  Output: a string representing the input in text form
         *  Method: if greater than 100, adds onesList word for the hundred number
         *          and "hundred
         */
        public string ThreeToText(string s, bool affixAnd)
        {
            int n = Convert.ToInt16(s);

            if (s == ZEROES)
            {
                return "";
            }
            if (n % 100 == 0)
            {
                return onesList[(n / 100)] + SPACE + HUNDRED;
            }
            else
                if (n >= 100)
                {
                    return onesList[(n / 100)] + SPACE + HUNDRED + SPACE + AND + TwoToText(Convert.ToString(n % 100));
                }
                else if (affixAnd)
                {
                    return AND + TwoToText(Convert.ToString(n % 100));
                }
                else
                {
                    return TwoToText(Convert.ToString(n % 100));
                }

        }

        //splitStringIntoBlocks
        //Seperate dollar value into groups of x, with odd number occuring at front
        //Eg. "1234567", with block size 3 becomes {"1", "234", "567"}
        public List<string> splitStringIntoBlocks(string str, int blocksize)
        {
            List<string> blockList = new List<string>();
            int firstBlockSize = str.Length % blocksize;
            if (firstBlockSize != 0)
            {
                blockList.Add(str.Substring(0, firstBlockSize));
            }
            for (int i = firstBlockSize; i < str.Length; i += blocksize)
            {
                blockList.Add(str.Substring(i, blocksize));
            }

            return blockList;
        }

    }
}
