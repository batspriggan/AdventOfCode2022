// See https://aka.ms/new-console-template for more information
using AdventOfCodeCommon;

var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(AdventOfCodeDay).IsAssignableFrom(p) && p.Name != typeof(AdventOfCodeDay).Name);

foreach (var type in types)
{
    var day = Activator.CreateInstance(type) as AdventOfCodeDay;
    day!.Year = 2021;
    Console.WriteLine(day.DayResults);
}