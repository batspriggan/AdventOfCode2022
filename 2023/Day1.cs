using System.Numerics;
using AdventOfCodeCommon;
namespace AdventOfCode;


internal class Day1 : AdventOfCodeDay
{
    public override int DayNumber => 1;
    
    public override string Calculate_1()
    {
        int total = 0;
        foreach (var line in ReadDayFile())
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                total += getNumber(line);
            }
        }
        return total.ToString();
    }
    private Dictionary<string, string> numberString = new Dictionary<string, string>
    {
        {"one", "one1one"},
        {"two","two2two"},
        {"three","three3three"},
        {"four","four4four"},
        {"five","five5five"},
        {"six","six6six"},
        {"seven","seven7seven"},
        {"eight","eight8eight"},
        {"nine","nine9nine"},
        {"zero","zero0zero"}
    };
    private string replaceNumberString(string line)
    {
        var newline = line;
        foreach(var x in numberString)
        {
            newline = newline.Replace(x.Key, x.Value);
        }
        return newline;
    }

    private int getNumber(string line)
    {
        var first = line.FirstOrDefault(x => Char.IsNumber(x));
        var last = line.LastOrDefault(x => Char.IsNumber(x));
        if (first == '\0' || last == '\0')
                return 0;
        var number = (first - 48) * 10 + last - 48;
        Console.WriteLine($"{line},{first},{last},{number}");
        return number;
    }
    public override string Calculate_2()
    {
        int total = 0;
        foreach (var line in ReadDayFile())
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                total += getNumber(replaceNumberString(line));
            }
        }
        return total.ToString();
    }
}
