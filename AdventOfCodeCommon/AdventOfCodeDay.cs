using System.Diagnostics;
namespace AdventOfCodeCommon;

public abstract class AdventOfCodeDay
{
    public int Year { get; set; }
    public abstract int DayNumber { get; }
    public abstract string Calculate_1();
    public abstract string Calculate_2();

    public string DayResults
    {
        get
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var first = Calculate_1();
            stopwatch.Stop();
            var firstime = stopwatch.Elapsed.ToString("mm\\:ss\\.fff");

            stopwatch = Stopwatch.StartNew();
            var second = Calculate_2();
            stopwatch.Stop();
            var secondtime = stopwatch.Elapsed.ToString("mm\\:ss\\.fff");

            return $"Results for Day {DayNumber} ({ReferenceUrl}) :\n" +
                   $"\tfirst result : {first} Elapsed : {firstime}\n" +
                   $"\tsecond result : {second} Elapsed : {secondtime}";
        }
    }


    public IEnumerable<string> ReadDayFile()
    {
        return File.ReadAllLines($"Day{DayNumber}.txt");
    }

    internal string ReferenceUrl => $"https://adventofcode.com/{Year}/day/{DayNumber}";
}