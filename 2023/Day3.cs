using AdventOfCodeCommon;
using System.Data;

namespace AdventOfCode;



internal class Day3 : AdventOfCodeDay
{
    public override int DayNumber => 3;
    private string numbers = "0123456789";
    
    public override string Calculate_1()
    {
        var data = ReadDayFile().ToList();
        int sum = 0;
        for(int i = 0 ; i < data.Count(); ++i )
        {
            string row = data[i];
            string currentNumber = "";
            bool addIt = false;
            for (int j = 0; j < row.Length; ++j)
            {
                if (!numbers.Contains(row[j]))//ignore non numbers
                {
                    if (!string.IsNullOrEmpty(currentNumber) && addIt)
                        sum += int.Parse(currentNumber);
                    currentNumber = "";
                    addIt = false;
                    continue;
                }
                currentNumber += row[j];
                if (AnyAdjacentSymbol(data, i, j).Item1)
                    addIt = true;
            }
            if (!string.IsNullOrEmpty(currentNumber) && addIt)
                sum += int.Parse(currentNumber);

        }
        return sum.ToString();
    }

    (bool,int?,int?) AnyAdjacentSymbol(List<string> data, int i, int j, bool onlyStar = false)
    {
        int[] dx = { -1, 0, 1, 0, -1, 1, -1, 1 };
        int[] dy = { 0, -1, 0, 1, -1, -1, 1, 1 };

        for (int k = 0; k < dx.Length; k++)
        {
            int ni = i + dx[k];
            int nj = j + dy[k];

            if (ni >= 0 && ni < data.Count && nj >= 0 && nj < data[i].Length)
            {
                char val = data[ni][nj];
                if (val == '*')
                    return (true, ni, nj);
                if(!numbers.Contains(val) && val != '.' && !onlyStar)
                    return (true,null,null);
            }
        }
        return (false,null,null);
    }
    

    public override string Calculate_2()
    {
        var data = ReadDayFile().ToList();
        List<(int, int, int)> allAdjacentToStar = new List<(int, int, int)>();
        for (int i = 0; i < data.Count(); ++i)
        {
            string row = data[i];
            string currentNumber = "";
            int lastStarI = -1, lastStarJ = -1;
            for (int j = 0; j < row.Length; ++j)
            {
                if (!numbers.Contains(row[j]))
                {
                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        var toAdd = int.Parse(currentNumber);
                        if (lastStarI != -1 && lastStarJ != -1)
                            allAdjacentToStar.Add((toAdd, lastStarI, lastStarJ));
                    }
                    currentNumber = "";
                    lastStarI = -1;
                    lastStarJ = -1;
                    continue;
                }
                currentNumber += row[j];
                var adjacent = AnyAdjacentSymbol(data, i, j, true);
                if (adjacent.Item1 == true && adjacent.Item2 != null && adjacent.Item3 != null)
                {
                    lastStarI = adjacent.Item2.Value;
                    lastStarJ = adjacent.Item3.Value;
                }
            }
        }
   
        var group = allAdjacentToStar
           .GroupBy(tuple => (tuple.Item2, tuple.Item3))
           .Where(group => group.Count() > 1);
        var products= group
           .Select(group => group.Aggregate(1, (acc, tuple) => acc * tuple.Item1)
           );
        return products.Sum().ToString();
    }
}
