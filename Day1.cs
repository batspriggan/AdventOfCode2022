namespace AdventOfCode2022
{
    internal class Day1 : AdventOfCodeDay
    {
        public override int DayNumber => 1;
        List<int> sumValues = new List<int>();
        public override string Calculate_1()
        {

            sumValues.Clear();
            int currentSum = 0;
            foreach (var line in LoadDayFile())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sumValues.Add(currentSum);
                    currentSum = 0;
                    continue;
                }
                currentSum += int.Parse(line);
            }
            return sumValues.Max().ToString();
        }
        
        public override string Calculate_2()
        {
            var _ = Calculate_1();
            sumValues.Sort();
            sumValues.Reverse();
            var maxthree = sumValues.Take(3).Sum();
            return maxthree.ToString();
        }
    }
}
