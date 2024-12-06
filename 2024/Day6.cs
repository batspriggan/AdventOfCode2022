using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AdventOfCodeCommon;
namespace AdventOfCode;

class Day6Matrix
{
	List<List<char>> matrix = new();
	public void Add(List<char> line)
	{
		matrix.Add(line);
	}

	public char this[int x, int y]
	{
		get
		{
			if (x >= XDimension || y >= YDimension || x < 0 || y < 0)
				return 'O';
			return matrix[y][x];
		}
		set
		{
			if (x >= XDimension || y >= YDimension || x < 0 || y < 0)
				return;
			matrix[y][x] = value;
		}
	}

	public int XDimension { get => matrix.Count; }
	public int YDimension { get => matrix[0].Count; }

	public (int x, int y) SearchGuard()
	{
		for (int x = 0; x < XDimension; x++)
		{
			for (int y = 0; y < YDimension; ++y)
			{
				if (this[x, y] == '^')
				{
					this[x, y] = '.';
					return (x, y);
				}
			}
		}
		return (0, 0);
	}

	List<(int x, int y)> directions = new List<(int x, int y)> {
		(0,-1),
		(1,0),
		(0,1),
		(-1,0)
	};

	private int NextDir(int index) => index + 1 >= directions.Count ? 0 : (index + 1);

	private bool NextIsObstacle(int x, int y, int xdir, int ydir) => this[x + xdir, y + ydir] == '#';
	private bool NextIsOut(int x, int y, int xdir, int ydir) => this[x + xdir, y + ydir] == 'O';
	private bool IsOut(int x, int y) => this[x, y] == 'O';

	public int Run(int x, int y, bool canHaveLoop = false)
	{
		int direction = 0;
		int currentx = x;
		int currenty = y;
		Dictionary<(int x, int y), bool> visits = new Dictionary<(int x, int y), bool>();
		visits[(currentx, currenty)] = true;
		bool ended = false;
		int iteration = 0;
		while (!ended)
		{
			if (canHaveLoop && iteration >= this.XDimension * this.YDimension)
				return -1;
			var curdir = directions[direction];
			if (NextIsOut(currentx, currenty, curdir.x, curdir.y))
				break;
			if (NextIsObstacle(currentx, currenty, curdir.x, curdir.y))
			{
				direction = NextDir(direction);
			}
			else
			{
				currentx += directions[direction].x;
				currenty += directions[direction].y;
				visits[(currentx, currenty)] = true;
			}
			++iteration;
			//DisplayData(currentx, currenty);
		}
		return visits.Count();
	}

	public (int x, int y) AddObstacle(int x, int y)
	{
		int i = x;
		for (int j = y; j < this.YDimension; ++j)
		{
			for (; i < this.XDimension; ++i)
			{
				if (this[i,j] == '#')
					continue;
				this[i,j] = '#';
				return (i,j);
			}
			i = 0;
		}
		return (-1, -1);
	}
	private void RemoveObstacle(int x, int y)
	{
		this[x, y] = '.';
	}

	public (int x, int y) Next(int x, int y)
	{
		if (x >= XDimension)
			return (0, y + 1);
		else
			return (x + 1, y);
	}

	public int FindObstacleNumber(int guardx, int guardy)
	{
		int number = 0;
		bool ended = false;
		int currentx = 0;
		int currenty = 0;
		while (!ended)
		{
			(currentx, currenty) = AddObstacle(currentx, currenty);
			if (Run(guardx, guardy, true) == -1)
				++number;

			ended = IsOut(currentx, currenty);

			RemoveObstacle(currentx, currenty);
			(currentx, currenty) = Next(currentx, currenty);
		}
		return number;
	}

	private void DisplayData(int guardx, int guardy)
	{
		Console.Clear();
		for (int y = 0; y < YDimension; ++y)
		{
			for (int x = 0; x < XDimension; ++x)
			{
				if (x == guardx && y == guardy)
					Console.Write("*");
				else
					Console.Write(this[x, y]);
			}
			Console.Write("\n");
		}
		Thread.Sleep(10);
	}

}

internal class Day6 : AdventOfCodeDay
{
	public override int DayNumber => 6;

	public override string Calculate_1()
	{
		int total = 0;
		var matrix = new Day6Matrix();
		foreach (var line in ReadDayFile())
		{
			matrix.Add(line.ToList());
		}
		var guard = matrix.SearchGuard();
		Stopwatch stopwatch = Stopwatch.StartNew();
		total = matrix.Run(guard.x, guard.y);
		stopwatch.Stop();	
		return total.ToString() + $" ExecTime = {stopwatch.Elapsed.TotalMilliseconds}";
	}

	public override string Calculate_2()
	{
		var matrix = new Day6Matrix();
		foreach (var line in ReadDayFile())
		{
			matrix.Add(line.ToList());
		}
		Stopwatch stopwatch = Stopwatch.StartNew();
		var guard = matrix.SearchGuard();
		var number = matrix.FindObstacleNumber(guard.x, guard.y);
		stopwatch.Stop();	
		return number.ToString() + $" ExecTime = {stopwatch.Elapsed.TotalMilliseconds}";
	}
}
