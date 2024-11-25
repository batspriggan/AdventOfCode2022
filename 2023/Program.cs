using AdventOfCodeCommon;

var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(AdventOfCodeDay).IsAssignableFrom(p) && p.Name != typeof(AdventOfCodeDay).Name);


List<AdventOfCodeDay> Instances = new List<AdventOfCodeDay>();
foreach (var type in types)
{
    var day = (AdventOfCodeDay)Activator.CreateInstance(type);
    day.Year = 2023;
    Instances.Add(day);

}

Instances.OrderBy(x => x.DayNumber).ToList().ForEach(x => Console.WriteLine(x.DayResults));