using AdventOfCodeCommon;
using System.Data;
using System.Drawing;

namespace AdventOfCode;

internal class Day12 : AdventOfCodeDay
{
    public override int DayNumber => 12;
    private  int mapWidth,mapHeight;
    private Stack<Movement> movementList  = new Stack<Movement>();

    record Movement(Point Current, List<Direction> Visited);

    public override string Calculate_1()
    {
        //not working
        var map = ParseFile();
        mapWidth = map.GetLength(0);
        mapHeight= map.GetLength(1);
        Point currentPosition = startPoint;
        int movements = 0;
        List<Movement> movementList = new();
        movementList.Add(new Movement(Current:startPoint,Visited:new()));
        while (currentPosition != arrivalPoint)
        {
            DrawMap(map, movementList);
            Movement prevMove = movementList[movementList.Count - 1];
            var nextPosition = MakeMove(map, prevMove.Current, prevMove);
            if (nextPosition != null)
            {
                currentPosition = nextPosition.Value;
                movementList.Add(new Movement(Current: currentPosition, Visited: new()));
                ++movements;
                continue;
            }
            else
            {
                --movements;
                movementList.RemoveAt(movementList.Count - 1);
            }
        }
        return movements.ToString();
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    Point? MakeMove(int[,] map, Point current, Movement prevMovement)
    {
        var x = current.X;
        var y = current.Y;
        var currElevation = map[x, y];

        var nextRight = (new Point(x, y + 1), Direction.Right);
        var nextUp = (new Point(x + 1, y), Direction.Up);
        var nextDown = (new Point(x - 1, y), Direction.Down);
        var nextLeft = (new Point(x, y - 1), Direction.Left);

        List<(Point, Direction)> tries = new List<(Point, Direction)>();
        if (!prevMovement.Visited.Contains(Direction.Up))
            tries.Add(nextUp);
        if (!prevMovement.Visited.Contains(Direction.Left))
            tries.Add(nextLeft);
        if (!prevMovement.Visited.Contains(Direction.Down))
            tries.Add(nextDown);
        if (!prevMovement.Visited.Contains(Direction.Right))
            tries.Add(nextRight);

        if (tries.Count == 0)
            return null;

        Point? nextCandidate = null;

        foreach (var next in tries)
        {
            if (next.Item1 == arrivalPoint)
                return next.Item1;

            if (OutOfBorder(next.Item1) || (map[next.Item1.X, next.Item1.Y] - currElevation) > 1 || next.Item1 == prevMovement.Current)
            {
                prevMovement.Visited.Add(next.Item2);
            }
            else
                nextCandidate = next.Item1;
        }

        return nextCandidate;
        
    }

    bool OutOfBorder(Point next)
    {
        if (next.Y >= mapHeight || next.X >= mapWidth || next.X < 0 || next.Y < 0)
            return true;
        return false;
    }

    void DrawMap(int[,] map, List<Movement> path)
    {
        Console.Clear();
        for (int i = 0; i < map.GetLength(0); ++i)
        {
            for (int j = 0; j < map.GetLength(1); ++j)
            {
                var current = new Point(i, j);
                int val = map[i, j];
                if (path.Exists(x => x.Current == current))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('x');
                }
                else if (val == 100)
                {
                    Console.Write('E');
                }
                else
                    Console.Write((char)('a' + val));
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
        Thread.Sleep(500);
    }


    Point arrivalPoint, startPoint;

    int[,] ParseFile()
    {
        var lines = ReadDayFile().ToArray();
        int height = lines.Count();
        int width = lines[0].Length;
        int[,] map = new int[height, width];
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                var chr = lines[i][j];
                if (chr == 'E')
                {
                    arrivalPoint = new Point(i, j);
                    map[i, j] = 100;
                }
                if (chr == 'S')
                {
                    startPoint = new Point(i, j);
                    map[i, j] = 0;
                }
                else
                    map[i, j] = lines[i][j] - 'a';
            }
        }
        return map;
    }

    public override string Calculate_2()
    {
        return "";
    }
}
