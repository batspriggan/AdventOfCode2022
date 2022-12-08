namespace AdventOfCode2022;
public record singleRecord(string first, string second);
internal class Day2 : AdventOfCodeDay
{
    public override int DayNumber => 2;

    public override string Calculate_1()
    {
        var inputData = ParseFile();
        var totalSum = inputData.Sum(x => scoresFirstStage[x]);
        return totalSum.ToString();
    }

    private Dictionary<singleRecord, int> scoresFirstStage = new Dictionary<singleRecord, int>()
    {
        { new singleRecord("A","X"),  3 + 1},
        { new singleRecord("A","Y"),  6 + 2},
        { new singleRecord("A","Z"),  0 + 3},
        { new singleRecord("B","X"),  0 + 1},
        { new singleRecord("B","Y"),  3 + 2},
        { new singleRecord("B","Z"),  6 + 3},
        { new singleRecord("C","X"),  6 + 1},
        { new singleRecord("C","Y"),  0 + 2},
        { new singleRecord("C","Z"),  3 + 3},
    };

    public override string Calculate_2()
    {
        var inputData = ParseFile();
        var totalSum = inputData.Sum(x => scoresSecondStage[x]);
        return totalSum.ToString();
    }

    private Dictionary<singleRecord, int> scoresSecondStage = new Dictionary<singleRecord, int>()
    {
        { new singleRecord("A","X"),  0 + 3},
        { new singleRecord("A","Y"),  3 + 1},
        { new singleRecord("A","Z"),  6 + 2},
        { new singleRecord("B","X"),  0 + 1},
        { new singleRecord("B","Y"),  3 + 2},
        { new singleRecord("B","Z"),  6 + 3},
        { new singleRecord("C","X"),  0 + 2},
        { new singleRecord("C","Y"),  3 + 3},
        { new singleRecord("C","Z"),  6 + 1},
    };

    public List<singleRecord> ParseFile()
    {
        return ReadDayFile().Select(x =>
        {
            var splitted = x.Split(' ');
            var first = splitted.First();
            var second = splitted.Last();
            return new singleRecord(first, second);
        }).ToList();
    }
}
