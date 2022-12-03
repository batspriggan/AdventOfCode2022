// See https://aka.ms/new-console-template for more information
using AdventOfCode2022;

var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IAdventOfCodeDay).IsAssignableFrom(p) && p.Name != typeof(IAdventOfCodeDay).Name);

foreach (var type in types)
{
    var day = (IAdventOfCodeDay)Activator.CreateInstance(type);
    Console.WriteLine(day.Calculate_1());
    Console.WriteLine(day.Calculate_2());
}