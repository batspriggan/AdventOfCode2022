using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2022;
internal class Day3 : AdventOfCodeDay
{
    public override int DayNumber => 3;
    private record ruckSack(string firstCompartment, string secondCompartment)
    { 
        public string AllItems => firstCompartment+ secondCompartment;
    };
    public int ToItemPriority(char c)
    {
        if (Convert.ToInt16(c) > 'Z')
            return c - 'a' + 1;
        return c - 'A' + 27;
    }
    public override string Calculate_1()
    {
        var itemsList = ParseFile();
        var totalSum = itemsList.Sum(item =>{
            var commonElement = item.firstCompartment.Intersect(item.secondCompartment).Single();
            return ToItemPriority(commonElement);
        });
        return totalSum.ToString();
    }

    public override string Calculate_2()
    {
        var itemsList = ParseFile().ToArray();
        int totalSum = 0;
        for(int i = 0; i < itemsList.Count(); i+=3)
        {
            var onlyThree = itemsList[i..(i+3)];
            var commonElement = onlyThree[0].AllItems.Intersect(onlyThree[1].AllItems).Intersect(onlyThree[2].AllItems).Single();
            totalSum += ToItemPriority(commonElement);
        }
        return totalSum.ToString();
    }

    List<ruckSack> ParseFile()
    {
        var allLines = File.ReadAllLines(@"Day3.txt");
        return allLines.Select(line =>{
            var firstHalf = line[..(line.Length / 2)];
            var secondHalf = line[(line.Length / 2)..];
            return new ruckSack(firstHalf,secondHalf);
        }).ToList();
    }
}
