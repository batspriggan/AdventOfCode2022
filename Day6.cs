using System.Data;
using System.Linq;

namespace AdventOfCode2022;
internal class Day6 : AdventOfCodeDay
{
    public override int DayNumber => 6;
    
    public override string Calculate_1()
    {
        var stream = LoadDayFile().ToArray()[0];
        var startCom = FindSomething(stream,4);
        return startCom.ToString();
    }

    private int FindSomething(string input, int packetLenght)
    {
        for (int i = 0; i < input.Length-3; ++i)
        {
            Dictionary<char, int> localdict = new Dictionary<char, int>();
            var substring = input.Substring(i, packetLenght);
            var allchar = substring.ToCharArray();
            foreach(var c in allchar)
            {
                localdict[c] = c;
                if (localdict.Keys.Count == packetLenght)
                    return i+packetLenght;
            }
        }
        return 0;
    }

    public override string Calculate_2()
    {
        var stream = LoadDayFile().ToArray()[0];
        var startCom = FindSomething(stream, 14);
        return startCom.ToString();
    }


}
