using AdventOfCodeCommon;
using System.Drawing;
using System.Net.Http.Headers;

namespace AdventOfCode;

internal class Day9 : AdventOfCodeDay
{
    public override int DayNumber => 9;

    public override string Calculate_1()
    {
        return MakeCalculation(new Rope(2), ParseMovements()).ToString();
    }

    void DrawKnots(Rope rope)
    {
        for (int i = -20; i < 20; ++i)
        {
            for (int j = -20; j < 20; ++j)
            {
                bool drawn = false;
                if (i == 0 && j == 0)
                {
                    Console.Write('s');
                    drawn = true;
                }
                for (int z = 0; z < rope.Knots.Length; ++z)
                {
                    if (rope.Knots[z].X == i && rope.Knots[z].Y == j)
                    {
                        drawn = true;
                        if (z == 0)
                            Console.Write('H');
                        else if (z == rope.Knots.Length)
                            Console.Write('T');
                        else
                            Console.Write(z.ToString());
                    }
                }
                if (!drawn)
                    Console.Write('.');
            }
            Console.WriteLine();
        }
        Thread.Sleep(500);
        Console.Clear();
    }

    class Rope
    {
        public Rope(int knots)
        {
            Knots = new Point[knots];
            for (int i = 0; i < Knots.Length; ++i)
                Knots[i] = new Point(0, 0);
        }
        public Point[] Knots;
        public Point Tail { get { return Knots[Knots.Length-1]; } }
    }

    record Movement(int X, int Y, int MovNum);
    List<Movement> ParseMovements()
    {
        List<Movement> movements = new List<Movement>();
        movements = ReadDayFile().ToArray().Select(line =>
        {
            var splitted = line.Split(' ');
            switch (splitted[0])
            {
                case "U": return new Movement(0, -1, int.Parse(splitted[1]));
                case "D": return new Movement(0, +1, int.Parse(splitted[1]));
                case "L": return new Movement(-1, 0, int.Parse(splitted[1]));
                case "R": return new Movement(1, 0, int.Parse(splitted[1]));
            }
            throw new Exception("foo");
        }).ToList();
        return movements;
    }

    int MakeCalculation(Rope rope, List<Movement> movements)
    {
        HashSet<Point> visitedPositions = new();
        foreach (var movement in movements)
        {
            for (int i = 0; i < movement.MovNum; ++i)
            {
                rope.Knots[0].X += movement.X;
                rope.Knots[0].Y += movement.Y;
                for (int j = 1; j < rope.Knots.Length; ++j)
                {
                    if (Math.Abs(rope.Knots[j - 1].X - rope.Knots[j].X) > 1 || Math.Abs(rope.Knots[j - 1].Y - rope.Knots[j].Y) > 1)
                    {
                        rope.Knots[j].X += Math.Min(Math.Max(-1, rope.Knots[j - 1].X - rope.Knots[j].X), 1);
                        rope.Knots[j].Y += Math.Min(Math.Max(-1, rope.Knots[j - 1].Y - rope.Knots[j].Y), 1);
                    }
                    if (!visitedPositions.Contains(rope.Tail))
                        visitedPositions.Add(rope.Tail);
                }
                //DrawKnots(rope);
            }
        }
        return visitedPositions.Count;
    }

    public override string Calculate_2()
    {
        return MakeCalculation(new Rope(10), ParseMovements()).ToString();
    }
}
