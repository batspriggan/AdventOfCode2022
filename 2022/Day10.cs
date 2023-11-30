using AdventOfCodeCommon;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;

namespace AdventOfCode;

internal class Day10 : AdventOfCodeDay
{
    public override int DayNumber => 10;

    public override string Calculate_1()
    {
        var allCommands = ParseFile();
        var totalSignalStrength = CalculateCycles(allCommands);
        return totalSignalStrength.ToString();
    }

    private int CalculateCycles(List<Command> allCommands)
    {
        int x = 1;
        int commandIndex = 0;
        int cycle = 1;
        int totalSum = 0;
        List<int> cycles = new List<int>() { 20, 60, 100, 140, 180, 220 };
        while(commandIndex<allCommands.Count) 
        {
            var command = allCommands[commandIndex];
            if (command.Instruction == "noop")
                ++commandIndex;
            else
            {
                --command.ExecutionCycles;
                if (command.ExecutionCycles == 0)
                {
                    x += command.Increment;
                    ++commandIndex;
                }
            }

            if (cycles.Contains(cycle))
                totalSum += x * cycle;
            ++cycle;
        }
        return totalSum;
    }

    int ManageCycle(List<Command> executionQueue)
    {
        executionQueue.ForEach(x => --x.ExecutionCycles);
        var x = executionQueue.Where(x => x.ExecutionCycles == 0).Sum(x => x.Increment);
        
        executionQueue.RemoveAll(x => x.ExecutionCycles == 0);
        return x; 
    }

    class Command
    {
        public string Instruction = "";
        public int Increment = 0;
        public int ExecutionCycles = 0;
    }

    List<Command> ParseFile()
    {
        var lines = ReadDayFile();
        return lines.Select(line =>
        {
            var splitted = line.Split(' ');
            if (splitted[0] == "noop")
                return new Command { Instruction = splitted[0] };
            else
                return new Command { Instruction = splitted[0], Increment = int.Parse(splitted[1]), ExecutionCycles = 2 };
        }).ToList();
    }

    public override string Calculate_2()
    {
        return "";
    }
}
