using AdventOfCodeCommon;
using System.Text.RegularExpressions;
namespace AdventOfCode;


internal class Day3 : AdventOfCodeDay
{
    public override int DayNumber => 3;

    public override string Calculate_1()
    {
        int total = 0;
        foreach (var line in ReadDayFile())
        {
            string pattern = @"mul\((\d+),(\d+)\)";

            MatchCollection matches = Regex.Matches(line, pattern);

            foreach (Match match in matches)
            {
                total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }
        return total.ToString();
    }

    public override string Calculate_2()
    {
        int total = 0;
        bool enabled = true;
        foreach (var line in ReadDayFile())
        {
            string pattern = @"(?:don't|do)\(\)|mul\((\d+),(\d+)\)";
            MatchCollection matches = Regex.Matches(line, pattern, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                string matchedValue = match.Groups[0].Value;
                if (matchedValue.StartsWith("mul") && enabled)
                    total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                else if (matchedValue == "do()")
                {
                    enabled = true;
                }
                else if (matchedValue == "don't()")
                {
                    enabled = false;
                }
            }
        }
        return total.ToString();
    }
}
