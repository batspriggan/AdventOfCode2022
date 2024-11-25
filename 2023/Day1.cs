using AdventOfCodeCommon;
namespace AdventOfCode;


internal class Day1 : AdventOfCodeDay
{
    public override int DayNumber => 1;
    List<int> sumValues = new List<int>();
    public override string Calculate_1()
    {
        return null;
        sumValues.Clear();
        int currentSum = 0;
        foreach (var line in ReadDayFile())
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                var first = line.First(x => Char.IsNumber(x));
                var last = line.Last(x => Char.IsNumber(x));
                sumValues.Add(currentSum);
                currentSum = 0;
                continue;
            }
            currentSum += int.Parse(line);
        }
        return sumValues.Max().ToString();
    }

    //int FindFirstNumber(string line)
    //{
    //    string number = "";
    //    var first = line.IndexOf(x => Char.IsNumber(x));
    //    foreach (var item in line)
    //    {
    //        var first = line.First(x => Char.IsNumber(x));
    //        number += first;
    //    }
    //}



    public override string Calculate_2()
    {
        return null;
        var _ = Calculate_1();
        sumValues.Sort();
        sumValues.Reverse();
        var maxthree = sumValues.Take(3).Sum();
        return maxthree.ToString();
    }
}
