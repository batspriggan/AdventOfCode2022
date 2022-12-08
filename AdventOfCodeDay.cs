namespace AdventOfCode2022
{
    internal abstract class AdventOfCodeDay
    {
        public abstract int DayNumber { get; }
        public abstract string Calculate_1();
        public abstract string Calculate_2();

        public string DayResults => $"Results for Day {DayNumber} ({ReferenceUrl}) :\n first result : {Calculate_1()}\n second result : {Calculate_2()}";
        
        internal IEnumerable<string> ReadDayFile()
        {
            return File.ReadAllLines($"Day{DayNumber}.txt");
        }

        internal string ReferenceUrl => $"https://adventofcode.com/2022/day/{DayNumber}";
    }
}