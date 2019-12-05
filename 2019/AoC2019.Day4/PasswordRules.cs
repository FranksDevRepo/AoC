using System.Linq;

namespace AoC2019.Day4
{
    internal class PasswordRules
    {
        internal static bool IsValidPassword(string password)
        {
            int[] numbers = ConvertStringToNumber(password);
            //var digitsNeverDecrease = (numbers.Select((x, index) => x - index).Where(n => n < 0).Count() == 0);
            bool digitsNeverDecrease = CheckDigitsNeverDecrease(numbers);
            //var adjacentDigits = numbers.Select((x, index) => x - index).Where(n => n == 0).Any();
            bool adjacentDigits = CheckAdjacentDigits(numbers);
            //bool isConsecutive = !myIntList.Select((i, j) => i - j).Distinct().Skip(1).Any();
            bool lengthIs6 = password.Length == 6;
            return digitsNeverDecrease && adjacentDigits && lengthIs6;
        }

        private static int[] ConvertStringToNumber(string password)
        {
            return password.Select(c => int.Parse(c.ToString())).ToArray();
        }

        private static bool CheckAdjacentDigits(int[] numbers)
        {
            bool adjacentDigits = false;
            var previousNumber = numbers.Take(1).First();
            foreach (var number in numbers.Skip(1))
            {
                if (number == previousNumber)
                {
                    adjacentDigits = true;
                    break;
                }
                previousNumber = number;
            }
            return adjacentDigits;
        }

        private static bool CheckDigitsNeverDecrease(int[] numbers)
        {
            bool digitsNeverDecrease = true;
            var previousNumber = numbers.Take(1).First();
            foreach (var number in numbers.Skip(1))
            {
                if (number < previousNumber)
                {
                    digitsNeverDecrease = false;
                    break;
                }
                previousNumber = number;
            }
            return digitsNeverDecrease;
        }

        public static int CountValidPasswordsInRange(int start, int end)
        {
            int count = 0;
            for (int number = start; number <= end; number++)
            {
                if (IsValidPassword(number.ToString()))
                    count++;
            }
            return count;
        }

        public static bool IsValidPasswordPuzzle2(string password)
        {
            int[] numbers = ConvertStringToNumber(password);
            bool digitsNeverDecrease = CheckDigitsNeverDecrease(numbers);
            bool max2AdjacentDigits = CheckMax2AdjacentDigits(numbers);
            bool lengthIs6 = password.Length == 6;
            return digitsNeverDecrease && max2AdjacentDigits && lengthIs6;

        }

        private static bool CheckMax2AdjacentDigits(int[] numbers)
        {
            bool max2AdjacentDigits = false;
            int count = 0;
            var previousNumber = numbers.Take(1).First();
            foreach (var number in numbers.Skip(1))
            {
                if (number == previousNumber)
                {
                    count++;
                }
                else
                {
                    if (count == 1) break;
                    else count = 0;
                }
                previousNumber = number;
            }
            max2AdjacentDigits = (count == 1);
            return max2AdjacentDigits;
        }

        public static int CountValidPasswordsInRangePuzzle2(int start, int end)
        {
            int count = 0;
            for (int number = start; number <= end; number++)
            {
                if (IsValidPasswordPuzzle2(number.ToString()))
                    count++;
            }
            return count;
        }
    }
}