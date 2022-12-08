namespace AdventOfCode2022;
internal class Day5 : AdventOfCodeDay
{
    public override int DayNumber => 5;
    internal record MoveCrates(int Qty, int From, int To);
    internal record Crates(int Row, Stack<char> ItemStack);

    public override string Calculate_1()
    {
        var (cratesList, movements) = ParseFile();
        movements.ForEach(x => CraneMover9000(cratesList, x));
        string answer = "";
        foreach (var crateStack in cratesList)
            answer += crateStack.ItemStack.Peek();
        return answer;
    }
    
    public override string Calculate_2()
    {
        var (cratesList, movements) = ParseFile();
        movements.ForEach(x => CraneMover9001(cratesList, x));

        string answer = "";
        foreach (var crateStack in cratesList)
            answer += crateStack.ItemStack.Pop();
        return answer;
    }

    internal void CraneMover9000(Crates[] cratesList, MoveCrates movement)
    {
        for (int i = 0; i < movement.Qty; ++i)
        {
            var crate = cratesList[movement.From].ItemStack.Pop();
            cratesList[movement.To].ItemStack.Push(crate);
        }
    }

    internal void CraneMover9001(Crates[] cratesList, MoveCrates movement)
    {
        Stack<char> tempStack = new Stack<char>();
        for (int i = 0; i < movement.Qty; ++i)
            tempStack.Push(cratesList[movement.From].ItemStack.Pop());
        tempStack.Reverse();
        foreach (var crane in tempStack)
            cratesList[movement.To].ItemStack.Push(crane);
    }

    (Crates[], List<MoveCrates>) ParseFile()
    {
        var allLines = ReadDayFile();
        var cratesStrings = allLines.Take(8).ToArray();
        var cratesList = new Crates[9];
        for (int z = 0; z < 9; ++z)
        {
            cratesList[z] = new Crates(z, new Stack<char>());
        }

        for (int j = 7; j >= 0; --j)
        {
            var crateRow = cratesStrings[j];
            for (int i = 0; i < crateRow.Length; i += 4)
            {
                var crateLetter = crateRow[i + 1];
                if (crateLetter != ' ')
                    cratesList[i / 4].ItemStack.Push(crateLetter);
            }
        }

        return new(cratesList, allLines.Skip(10).Select(line =>
        {
            var sections = line.Split(' ');
            return new MoveCrates(
                Qty: int.Parse(sections[1]),
                From: int.Parse(sections[3]) - 1,
                To: int.Parse(sections[5]) - 1);
        }).ToList());
    }
}
