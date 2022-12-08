using AdventOfCodeCommon;
namespace AdventOfCode;
internal class Day6 : AdventOfCodeDay
{
    public override int DayNumber => 6;

    public override string Calculate_1()
    {
        return Calculate(80).ToString();
    }

    public long Calculate(int days)
    {
        var allLines = ReadDayFile().ToArray();
        var fishes = allLines[0].Split(',').GroupBy(x => x).ToDictionary(x => int.Parse(x.Key), x => (long)x.Count());

        for (int i = 0; i < 9; ++i)
        {
            if (!fishes.Keys.Contains(i))
                fishes[i] = 0;
        }

        for (int i = 0; i < days; ++i)
        {
            long fishesAt0 = fishes[0];
            for (int j = 1; j < 9; ++j)
            {
                fishes[j - 1] = fishes[j];
            }
            fishes[6] += fishesAt0;
            fishes[8] = fishesAt0;
        }
        return fishes.Sum( x=> x.Value);
    }

    public override string Calculate_2()
    {
        return Calculate(256).ToString();
    }


}
