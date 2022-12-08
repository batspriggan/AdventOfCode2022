using AdventOfCodeCommon;
namespace AdventOfCode;

internal class Day8 : AdventOfCodeDay
{
    public override int DayNumber => 8;

    public override string Calculate_1()
    {
        var treeGrid = ParseGridFile();
        int visibleCount = 0;
        for (int i = 0; i < treeGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < treeGrid.GetLength(1); ++j)
            {
                var (visible, _) = IsVisibleFromTheBorder(i, j, treeGrid);
                visibleCount += visible ? 1 : 0;
            }
        }
        return visibleCount.ToString();
    }

    int[,] ParseGridFile()
    {
        var allLines = ReadDayFile().ToArray();
        var width= allLines[0].Length;
        var height = allLines.Count();

        var grid = new int[width, height];

        for (int i = 0; i < height; ++i)
        {
            var line = allLines[i];
            for (int j = 0; j < width; ++j)
            {
                string v = line[j].ToString();
                grid[i,j] = int.Parse(v);
            }
        }
        //ShowGrid(grid);
        return grid;
    }

    void ShowGrid(int[,] treeGrid)
    {
        for (int i = 0; i < treeGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < treeGrid.GetLength(1); ++j)
                Console.Write(treeGrid[i, j]);
            Console.WriteLine();
        }
    }

    (bool,int) IsVisibleFromTheBorder(int x, int y, int[,] treeGrid)
    {
        var (visibleUp, scenicDistanceUp) = IsVisibleUp(x, y, treeGrid);
        var (visibleDown, scenicDistanceDown) = IsVisibleDown(x, y, treeGrid);
        var (visibleLeft, scenicDistanceLeft) = IsVisibleLeft(x, y, treeGrid);
        var (visibleRight, scenicDistanceRight) = IsVisibleRight(x, y, treeGrid);
        return (visibleDown | visibleUp | visibleLeft | visibleRight, scenicDistanceDown * scenicDistanceLeft * scenicDistanceRight * scenicDistanceUp);
    }

    (bool,int) IsVisibleUp(int row, int column, int[,] treeGrid)
    {
        if (row == 0)
            return (true, 0);
        var treeHeight = treeGrid[row, column];
        int scenicDistance = 0;
        for (int i = row-1; i >= 0; --i)
        {
            ++scenicDistance;
            if (treeGrid[i, column] >= treeHeight)
                return (false, scenicDistance);
        }
        return (true, scenicDistance);
    }

    (bool, int) IsVisibleDown(int row, int column, int[,] treeGrid)
    {
        var rows = treeGrid.GetLength(0);
        if (column == (rows - 1))
            return (true, 0);
        var treeHeight = treeGrid[row, column];
        int scenicDistance = 0;
        for (int i = row+1; i < rows; ++i)
        {
            ++scenicDistance;
            if (treeGrid[i, column] >= treeHeight)
                return (false, scenicDistance);
        }
        return (true, scenicDistance);
    }

    (bool, int) IsVisibleRight(int row, int column, int[,] treeGrid)
    {
        var columns= treeGrid.GetLength(1);
        if (column == (columns - 1))
            return (true, 0);
        var treeHeight = treeGrid[row, column];
        int scenicDistance = 0;
        for (int i = column+1; i < columns; ++i)
        {
            ++scenicDistance;
            if (treeGrid[row, i] >= treeHeight)
                return (false, scenicDistance);
        }
        return (true,scenicDistance);
    }

    (bool, int) IsVisibleLeft(int row, int column, int[,] treeGrid)
    {
        if (column == 0)
            return (true,0);
        var treeHeight = treeGrid[row, column];
        int scenicDistance = 0;
        for (int i = column-1; i >= 0 ; --i)
        {
            ++scenicDistance;
            if (treeGrid[row, i] >= treeHeight)
                return (false,scenicDistance);
        }
        return (true,scenicDistance);
    }

    public override string Calculate_2()
    {
        var treeGrid = ParseGridFile();
        int maxScenicDistance = 0;
        for (int i = 0; i < treeGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < treeGrid.GetLength(1); ++j)
            {
                var (_, scenicDistance) = IsVisibleFromTheBorder(i, j, treeGrid);
                if(scenicDistance> maxScenicDistance)
                    maxScenicDistance = scenicDistance;
            }
        }
        return maxScenicDistance.ToString();
    }
}
