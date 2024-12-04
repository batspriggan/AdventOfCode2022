using AdventOfCodeCommon;
namespace AdventOfCode;

class MyMatrix
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
			return matrix[x][y];
		}
	}

	public string Get3x3X(int x, int y)
	{
		return string.Concat(this[x, y], this[x, y + 2], this[x + 1, y + 1], this[x + 2, y], this[x + 2, y + 2]);
	}

	List<(int x, int y)> increments = new()
	{
		(-1, -1),
		(-1, 0),
		(-1, 1),

		(0, -1),
		(0, 1),

		(1, -1),
		(1, 0),
		(1, 1)
	};

	public int XDimension { get => matrix.Count; }
	public int YDimension { get => matrix[0].Count; }

	List<char> words = new List<char>() { 'X', 'M', 'A', 'S' };

	public int SearchXmas(int x, int y)
	{
		int found = 0;
		if (this[x, y] == words[0])
		{
			foreach (var direction in increments)
			{
				bool oneFound = true;
				for (int z = 1; z <= words.Count - 1; ++z)
				{
					if (this[x + direction.x * z, y + direction.y * z] != words[z])
					{
						oneFound = false;
						break;
					}
				}
				if (oneFound)
					++found;

			}
		}
		return found;
	}
	public int SearchDoubleMas(int x, int y)
	{
		int found = 0;
		string a = Get3x3X(x, y);
		if (a == "MSAMS" || a == "SSAMM" || a == "SMASM" || a == "MMASS")
			++found;
		return found;
	}
}

internal class Day4 : AdventOfCodeDay
{
	public override int DayNumber => 4;

	public override string Calculate_1()
	{
		int total = 0;
		var matrix = new MyMatrix();
		foreach (var line in ReadDayFile())
		{
			matrix.Add(line.ToList());
		}
		for (int j = 0; j < matrix.YDimension; ++j)
		{
			for (int i = 0; i < matrix.XDimension; ++i)
			{
				total += matrix.SearchXmas(i, j);
			}
		}
		return total.ToString();
	}

	public override string Calculate_2()
	{
		int total = 0;
		var matrix = new MyMatrix();
		foreach (var line in ReadDayFile())
		{
			matrix.Add(line.ToList());
		}
		for (int j = 0; j < matrix.YDimension; ++j)
		{
			for (int i = 0; i < matrix.XDimension; ++i)
			{
				total += matrix.SearchDoubleMas(i, j);
			}
		}
		return total.ToString();
	}
}
