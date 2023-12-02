using System.Text.RegularExpressions;

namespace AOC2023.Day_1
{
    /// <summary>
    /// For a given text file, each line contains a calibration value which is derived by combining the FIRST and LAST digit in the line.
    /// </summary>
    /// <returns>
    /// First read each line in the text file, derive the calibration value and add it to the sum counter. Output the entire sum value.
    /// </returns>
    internal class CalibrationValue
    {
        private readonly string _filePath;
        //Create Dictionary to hold string int value pairs
        private readonly Dictionary<string, string> _wordInt = new()
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };
        public CalibrationValue(string filepath)
        {
            _filePath = filepath;
        }

        public int GetSumofCalibrationValues()
        {
            int sum1 = 0;
            int sum2 = 0;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                //Get calibration value
                var calValue2 = GetCalibratopmValueFromEachLineDigitsLetters(line);
                Console.WriteLine(calValue2.ToString());
                if (calValue2 != -1)
                { sum2 = sum2 + calValue2;
                    
                }
            }
            Console.WriteLine(" Sum2:\t" + sum2);
            return 1;

        }

        //Part One
        //parse each line and grab only the first and last digits
        private int GetCalibrationValueFromEachLine(string line)
        {
            var calValue = -1;
            //extract all digits from the line
            var digits = new string(line.Where(char.IsDigit).ToArray());

            calValue = digits.Length switch
            {
                1 => int.Parse(digits + digits),
                > 1 => int.Parse(digits.Substring(0,1) + digits.Substring(digits.Length-1,1)),
                _ => calValue
            };

            return calValue;
        }

        //Part Two
        //parse each line and grab the first and last digits even if digits can be actually spelled out with letters :
        //one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".
        private int GetCalibratopmValueFromEachLineDigitsLetters(string line)
        {
            //parse the line to extract all digits numeric or substring
            var calValue = -1;

            string pattern = string.Join("|", new List<string>(_wordInt.Keys).ToArray());

            var output = Regex.Replace(line, pattern, Replacer);

            string Replacer(Match match)
            {
                var value = match.Value;
                foreach (var kvp in _wordInt)
                {
                    if (value.Contains(kvp.Key)) value = value.Replace(kvp.Key, kvp.Value);
                }
                return value;
            }
            //extract all digits from the line

            var digits = new string(output.Where(char.IsDigit).ToArray());

            calValue = digits.Length switch
            {
                1 => int.Parse(digits+digits),
                > 1 => int.Parse((digits[0].ToString() + digits[digits.Length-1].ToString())),
                _ => calValue
            };
            return calValue;
        }

    }
}
