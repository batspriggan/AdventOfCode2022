using AdventOfCodeCommon;
namespace AdventOfCode;

internal class Day7 : AdventOfCodeDay
{
	public override int DayNumber => 7;
	readonly char[] symbols1 = new char[] { '*', '+' };
	readonly char[] symbols2 = new char[] { '*', '+', '|' };

	static List<string> GenerateCombinations(int n, char[] symbols)
	{
		List<string> results = new();
		int totalCombinations = (int)Math.Pow(symbols.Length, n);
		for (int i = 0; i < totalCombinations; i++)
		{
			string combination = "";
			int temp = i;

			for (int j = 0; j < n; j++)
			{
				combination = symbols[temp % symbols.Length] + combination;
				temp /= symbols.Length;
			}

			results.Add(combination);
		}

		return results;
	}

	bool CheckAnyOperatorGenerateResult(long[] numbers, long result, List<string> operators)
	{
		foreach (var oplist in operators)
		{
			long total = numbers[0];
			for (int i = 1; i < numbers.Length; ++i)
			{
				if (oplist[i - 1] == '|')
				{
					total = long.Parse($"{total}{numbers[i]}");
				}
				else if (oplist[i - 1] == '*')
					total *= numbers[i];
				else
					total += numbers[i];
			}
			if (result == total)
				return true;
		}
		return false;
	}

	public override string Calculate_1()
	{
		long total = 0;
		foreach (var line in ReadDayFile())
		{
			var splitted = line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var result = long.Parse(splitted[0]);
			var operators = GenerateCombinations(splitted.Length - 2, symbols1);
			if (CheckAnyOperatorGenerateResult(splitted.Skip(1).Select(x => long.Parse(x)).ToArray(), result, operators))
				total += result;
		}
		return total.ToString();
	}

	public override string Calculate_2()
	{
		long total = 0;
		foreach (var line in ReadDayFile())
		{
			var splitted = line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var result = long.Parse(splitted[0]);
			var operators = GenerateCombinations(splitted.Length - 2, symbols2);
			if (CheckAnyOperatorGenerateResult(splitted.Skip(1).Select(x => long.Parse(x)).ToArray(), result, operators))
				total += result;
		}
		return total.ToString();
	}
}
