namespace AdventOfCode2022
{
    internal class Day1 : IAdventOfCodeDay
    {
        List<int> sumValues = new List<int>();
        public string Calculate_1()
        {

            sumValues.Clear();
            int currentSum = 0;
            foreach (var line in ParseFile())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sumValues.Add(currentSum);
                    currentSum = 0;
                    continue;
                }
                currentSum += int.Parse(line);
            }
            return $"Day 1 first result : {sumValues.Max()}";
        }

        private IEnumerable<string> ParseFile()
        {
            return File.ReadAllLines(@"Day1.txt");
        }
        public string Calculate_2()
        {
            var _ = Calculate_1();
            sumValues.Sort();
            sumValues.Reverse();
            var maxthree = sumValues.Take(3).Sum();
            return $"Day 1 second result : {maxthree}";
        }
    }
}
