// See https://aka.ms/new-console-template for more information
using AdventOfCode2022;

var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(AdventOfCodeDay).IsAssignableFrom(p) && p.Name != typeof(AdventOfCodeDay).Name);

foreach (var type in types)
{
    var day = (AdventOfCodeDay)Activator.CreateInstance(type);
    Console.WriteLine(day.DayResults);
}