using System.Numerics;
using System.Reflection.Emit;
using AdventOfCodeCommon;
namespace AdventOfCode;


internal class Day1 : AdventOfCodeDay
{
    public override int DayNumber => 1;

    public override string Calculate_1()
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach (var line in ReadDayFile())
        {
            var splitted = line.Split(' ').ToList();
            list1.Add(int.Parse(splitted.First()));
            list2.Add(int.Parse(splitted.Last()));
        }
        list1 = list1.OrderBy(x => x).ToList();
        list2 = list2.OrderBy(x => x).ToList();
        int totalDistance = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            totalDistance += Math.Abs(list1[i] - list2[i]);
        }
        return totalDistance.ToString();

    }
    public override string Calculate_2()
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach (var line in ReadDayFile())
        {
            var splitted = line.Split(' ').ToList();
            list1.Add(int.Parse(splitted.First()));
            list2.Add(int.Parse(splitted.Last()));
        }
        int total = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            var similarity = list2.Count(x => x == list1[i]);
            total += list1[i] * similarity;
        }
        return total.ToString();
    }
}
