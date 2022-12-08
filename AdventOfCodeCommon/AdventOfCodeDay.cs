namespace AdventOfCodeCommon;

public abstract class AdventOfCodeDay
{
    public int Year { get; set; }
    public abstract int DayNumber { get; }
    public abstract string Calculate_1();
    public abstract string Calculate_2();

    public string DayResults => $"Results for Day {DayNumber} ({ReferenceUrl}) :\n first result : {Calculate_1()}\n second result : {Calculate_2()}";

    public IEnumerable<string> ReadDayFile()
    {
        return File.ReadAllLines($"Day{DayNumber}.txt");
    }

    internal string ReferenceUrl => $"https://adventofcode.com/{Year}/day/{DayNumber}";
}