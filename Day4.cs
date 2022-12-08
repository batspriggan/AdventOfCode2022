namespace AdventOfCode2022;
internal class Day4 : AdventOfCodeDay
{
    public override int DayNumber => 4;
    internal record Assignment(int From, int To);
    internal record AssignmentPair(Assignment FirstElf, Assignment SecondElf);
    public override string Calculate_1()
    {
        var allAssignments = ParseFile();
        var allOverlapped = allAssignments.Count(x => AnyContainsOther(x.FirstElf, x.SecondElf));
        return allOverlapped.ToString();
    }

    public override string Calculate_2()
    {
        var allAssignments = ParseFile();
        var allOverlapped = allAssignments.Count(x => Overlaps(x.FirstElf, x.SecondElf));
        return allOverlapped.ToString();
    }

    internal bool AnyContainsOther(Assignment first, Assignment second)
    {
        if ((second.From >= first.From && second.To <= first.To) ||
            (first.From >= second.From && first.To <= second.To))
            return true;
        return false;
    }

    internal bool Overlaps(Assignment first, Assignment second)
    {
        if (second.From <= first.To && first.From <= second.To)
            return true;
        return false;
    }

    List<AssignmentPair> ParseFile()
    {
        var allLines = ReadDayFile();
        return allLines.Select(line =>
        {
            var sections = line.Split(new char[] { ',', '-' });
            return new AssignmentPair(
                new Assignment(int.Parse(sections[0]), int.Parse(sections[1])),
                new Assignment(int.Parse(sections[2]), int.Parse(sections[3]))
                );
        }).ToList();
    }
}
