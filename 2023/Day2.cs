using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using AdventOfCodeCommon;
namespace AdventOfCode;


internal class Day2 : AdventOfCodeDay
{
	public override int DayNumber => 2;

	class Extraction
	{
		private Dictionary<string, int> ColorReference = new Dictionary<string, int>()
		{
			{ "red", 12},
			{ "green", 13},
			{ "blue", 14},
		};
		public Dictionary<string, int> ColorsMaxCube = new Dictionary<string, int>()
		{
			{ "red", 0},
			{ "green", 0},
			{ "blue", 0},
		};
		public bool Invalid() => ColorsMaxCube.Any(x => ColorReference[x.Key] < x.Value);
		

		public int CubePower()
		{
			int result = 1;
			foreach (var pair in ColorsMaxCube)
			{
				result *= pair.Value;
			}
			return result;
		}

	}

	public override string Calculate_1()
	{
		int total = 0;
		foreach (var line in ReadDayFile())
		{
			var game = line.Split(":");
			int gamenumber = int.Parse(game[0].Replace("Game ", ""));
			var parsed = parseTotalRGB(game[1]);
			if (!parsed.Invalid())
				total += gamenumber;

		}
		return total.ToString();
	}

	private Extraction parseTotalRGB(string line)
	{
		var ret = new Extraction();
		var extractions = line.Split(";");
		foreach (var extraction in extractions)
		{
			var colors = extraction.Split(",");

			foreach (var color in colors)
			{
				var elements = color.Trim().Split(" ");
				var colorName = elements[1];
				var quantity = int.Parse(elements[0]);
				ret.ColorsMaxCube[colorName] = int.Max(ret.ColorsMaxCube[colorName], quantity);
			}
		}
		return ret;
	}

	public override string Calculate_2()
	{
		int total = 0;
		foreach (var line in ReadDayFile())
		{
			var game = line.Split(":");
			int gamenumber = int.Parse(game[0].Replace("Game ", ""));
			var parsed = parseTotalRGB(game[1]);
			total += parsed.CubePower();
		}
		return total.ToString();
	}
}
