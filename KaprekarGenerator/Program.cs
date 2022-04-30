namespace Program
{
    class Program
    {
        /// <summary>
        /// Print out the starting number and the number of iterations it takes 
        /// to reach Kaprekar's constant when performing Kaprekar's routine.
        /// </summary>
        public static void Main()
        {
            // The algorithm is limited to 4 digit numbers.
            for (int i = 1; i <= 9999; i++)
            {
                var iterationsTaken = PerformKaprekarRoutine(i);

                Console.WriteLine($"Starting Number: {i}; Iterations Taken: {iterationsTaken}");
            }
        }

        /// <summary>
        /// Returns the number of iterations needed to reach Kaprekar's 
        /// constant, or -1 if the constant is not reached.
        /// </summary>
        /// <param name="startingNumber"></param>
        /// <returns>An integer between 1 and 7, or -1.</returns>
        private static int PerformKaprekarRoutine(int startingNumber)
        {
            if (
                startingNumber <= 0 ||
                Pad(startingNumber).Length > 4 ||
                !HasAtLeastTwoUniqueDigits(startingNumber)
               )
            {
                return -1;
            }

            const int KaprekarsConstant = 6174;

            var nextNumber = startingNumber;

            for (int iteration = 0; iteration < 7; iteration++)
            {
                var paddedNumber = Pad(nextNumber);

                var numberSortedDescending = int.Parse(string.Join("", paddedNumber.OrderByDescending(d => d)));
                var numberSortedAscending = int.Parse(string.Join("", paddedNumber.OrderBy(d => d)));

                var result = numberSortedDescending - numberSortedAscending;

                if (result == KaprekarsConstant)
                {
                    return iteration + 1;
                }

                nextNumber = result;
            }

            return -1;
        }

        private static string Pad(int unpaddedNumber)
        {
            var paddedNumber = unpaddedNumber.ToString();

            while (paddedNumber.Length < 4)
            {
                paddedNumber += "0";
            }

            return paddedNumber;
        }

        private static bool HasAtLeastTwoUniqueDigits(int number)
        {
            var paddedNumber = Pad(number);

            var uniques = new HashSet<char>();

            foreach (var d in paddedNumber)
            {
                uniques.Add(d);
            }

            return uniques.Count >= 2;
        }
    }
}
