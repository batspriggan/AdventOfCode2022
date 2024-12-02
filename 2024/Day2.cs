using System.Numerics;
using System.Reflection.Emit;
using AdventOfCodeCommon;
namespace AdventOfCode;


internal class Day2 : AdventOfCodeDay
{
    public override int DayNumber => 2;

    public class Pair
    {
        public int A { get; set; }
        public int B { get; set; }

        public int AbsDiff { get => Math.Abs(A - B); }
    }

    public override string Calculate_1()
    {
        int safeNumber = 0;
        foreach (var line in ReadDayFile())
        {
            var splitted = line.Split(' ').Select(x => int.Parse(x)).ToList();
            var zipped = splitted.Zip(splitted.Skip(1), (a, b) => new Pair { A = a, B = b }).ToList();
            if (IsSafe(zipped))
                ++safeNumber;
        }
        return safeNumber.ToString();
    }

    private bool IsSafe(List<Pair> list) => list.All(p => p.A < p.B && p.AbsDiff <= 3) || list.All(p => p.A > p.B && p.AbsDiff <= 3);

    public override string Calculate_2()
    {
        int safeNumber = 0;
        foreach (var line in ReadDayFile())
        {
            var splitted = line.Split(' ').Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < splitted.Count; i++)
            {
                var newArray = splitted.Where((_, index) => index != i).ToList();
                var zipped = newArray.Zip(newArray.Skip(1), (a, b) => new Pair { A = a, B = b }).ToList();
                if (IsSafe(zipped))
                {
                    ++safeNumber;
                    break;
                }
            }
        }
        return safeNumber.ToString();
    }
}
