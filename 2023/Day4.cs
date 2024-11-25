using AdventOfCodeCommon;
using System.Collections.Generic;
using System.Data;

namespace AdventOfCode;

internal class Day4 : AdventOfCodeDay
{
    public override int DayNumber => 4;

    List<(string, List<int>, List<int>)> numbers = new List<(string, List<int>, List<int>)>();

    List<(int, int)> ParseData(IEnumerable<string> data)
    {
        List<(int, int)> ret = new List<(int, int)>();
        foreach (var item in data)
        {
            var firstsub = item.Split('|');
            var secondsub = firstsub[0].Split(":");
            var itemData = WinningNumbersAndWeight(
                secondsub[1].Split(" ").Where(y => !string.IsNullOrWhiteSpace(y)).Select(x => int.Parse(x)).ToList(),
                                firstsub[1].Split(" ").Where(y => !string.IsNullOrWhiteSpace(y)).Select(x => int.Parse(x)).ToList()
                                );
            ret.Add(itemData);
        }
        return ret;
    }

    public override string Calculate_1()
    {
        var data = ParseData(ReadDayFile());
        return data.Sum(x => x.Item2).ToString();
    }
    
    public override string Calculate_2()
    {
        var data = ParseData(ReadDayFile()).Select(x => x.Item1).ToList();
        List<int> numberOfTickets = Enumerable.Repeat(1, data.Count).ToList();

        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 1; j <= data[i]; j++)
            {
                numberOfTickets[i + j] += numberOfTickets[i];
            }
        }
        return numberOfTickets.Sum().ToString();

    }

    (int,int) WinningNumbersAndWeight(List<int> winning, List<int> mine)
    {
        var intersection = winning.Intersect(mine);
        if (intersection.Count() == 0)
            return (0,0);
        return (intersection.Count(), intersection.Skip(1).Aggregate( 1, (a, b) => a * 2));
    }
}
