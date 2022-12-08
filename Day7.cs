namespace AdventOfCode2022;
internal class Day7 : AdventOfCodeDay
{
    public override int DayNumber => 7;

    public override string Calculate_1()
    {
        var (root,flattenedList) = ParseFileSystem();
        var total = flattenedList.Sum(x => { return x.TotalSize < 100000 ? x.TotalSize : 0; });
        ShowFolder(root);
        return total.ToString();
    }

    class Folder
    {
        public string Name { get; set; }
        public List<Folder> Subfolders { get; set; } = new List<Folder>();
        public Folder Parent { get; set; }
        
        public List<File> Files { get; set; } = new List<File>();
        public long TotalSize
        {
            get
            {
                var subFolderSize = Subfolders.Sum(x => x.TotalSize);
                return subFolderSize + MySize;
            }
        }
        public long MySize { get => Files.Sum(x => x.Size);}
    }
    class File
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }

    void ShowFolder(Folder folder, int depth = 0)
    {
        Console.WriteLine(new String(' ', depth*2) + folder.Name);
        foreach (var subfolder in folder.Subfolders)
        {
            ShowFolder(subfolder, depth + 1);
        }
        foreach (var file in folder.Files)
        {
            Console.WriteLine(new String(' ', depth*2) + file.Size + " " + file.Name);
        }
    }


    (Folder, List<Folder>) ParseFileSystem()
    {
        var allLines = LoadDayFile();

        List<Folder> flattenedFolderList = new List<Folder>();
        Folder rootFileSystem = new Folder { Name = "/" };
        Folder currentFolder = rootFileSystem;

        foreach (var line in allLines.Skip(1))
        {
            if (line == "$ cd ..")
            {
                currentFolder = currentFolder.Parent;
            }
            else if (line.StartsWith("$ cd "))
            {
                string folder = line.Replace("$ cd ", "");
                var newFolder = new Folder() { Name = folder };
                currentFolder.Subfolders.Add(newFolder);
                flattenedFolderList.Add(newFolder);
                newFolder.Parent = currentFolder;
                currentFolder = newFolder;
            }
            else if (line == "$ ls")
            { 
            }
            else if (line[0] != '$')
            {
                var elements = line.Split(' ');
                if (elements[0] != "dir")
                {
                    var newFile = new File { Name = elements[1], Size = long.Parse(elements[0]) };
                    currentFolder.Files.Add(newFile);
                }
            }
        }
        return (rootFileSystem, flattenedFolderList);
    }

    public override string Calculate_2()
    {
        long hddTotalSpace = 70000000;
        long requiredFreeSpace = 30000000;
        var (root, flattenedList) = ParseFileSystem();
        var currentFreeSpace = hddTotalSpace - root.TotalSize;
        long minspace = requiredFreeSpace;
        foreach (var folder in flattenedList)
        {
            if ( (currentFreeSpace + folder.TotalSize) > requiredFreeSpace)
            { 
                if(folder.TotalSize < minspace)
                    minspace = folder.TotalSize;
            }
        }
        return minspace.ToString();
    }
}
