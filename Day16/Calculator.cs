using System.Text;

namespace Day16;

public class Calculator
{
    public int[,] energizedArray;
    public int[,] energizedNorthArray;
    public int[,] energizedSouthArray;
    public int[,] energizedEastArray;
    public int[,] energizedWestArray;


    public string[,] pathArray;
    public int rowCount;
    public int columnCount;

    public void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "input.txt");


        var lines = File.ReadAllLines(path);

        rowCount = lines.Length;
        columnCount = lines[0].Length;

        pathArray = new string[rowCount, columnCount];


        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                pathArray[i, j] = lines[i][j].ToString();
            }
        }


        var resultList = new List<int>();

        for (int i = 0; i < rowCount; i++)
        {
            resultList.Add(PrepareSystemAndGoLight(i, 0, DirectionEnum.EAST));
            resultList.Add(PrepareSystemAndGoLight(i, columnCount - 1, DirectionEnum.WEST));
        }

        for (int i = 0; i < columnCount; i++)
        {
            resultList.Add(PrepareSystemAndGoLight(0, i, DirectionEnum.SOUTH));
            resultList.Add(PrepareSystemAndGoLight(rowCount - 1, i, DirectionEnum.NORTH));
        }

        Console.WriteLine(resultList.Max());
    }


    public int PrepareSystemAndGoLight(int startRow, int startColumn, DirectionEnum direction)
    {
        energizedArray = new int[rowCount, columnCount];
        energizedNorthArray = new int[rowCount, columnCount];
        energizedSouthArray = new int[rowCount, columnCount];
        energizedEastArray = new int[rowCount, columnCount];
        energizedWestArray = new int[rowCount, columnCount];


        GoLightGo(startRow, startColumn, direction);

        return PrintAndGetResult(startRow, startColumn);
    }

    public int PrintAndGetResult(int row, int column)
    {
        int sum = 0;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                sum += energizedArray[i, j];
            }
        }

        Console.WriteLine("StartPoint is (" + row + " , " + column + ")  sum is " + sum);

        return sum;
    }

    public void GoLightGo(int startRow, int startColumn, DirectionEnum direction)
    {
        bool isEnd = false;

        if (direction == DirectionEnum.NORTH)
        {
            if (energizedNorthArray[startRow, startColumn] != 1)
            {
                energizedNorthArray[startRow, startColumn] = 1;
            }
            else
            {
                return;
            }
        }
        else if (direction == DirectionEnum.SOUTH)
        {
            if (energizedSouthArray[startRow, startColumn] != 1)
            {
                energizedSouthArray[startRow, startColumn] = 1;
            }
            else
            {
                return;
            }
        }
        else if (direction == DirectionEnum.EAST)
        {
            if (energizedEastArray[startRow, startColumn] != 1)
            {
                energizedEastArray[startRow, startColumn] = 1;
            }
            else
            {
                return;
            }
        }
        else if (direction == DirectionEnum.WEST)
        {
            if (energizedWestArray[startRow, startColumn] != 1)
            {
                energizedWestArray[startRow, startColumn] = 1;
            }
            else
            {
                return;
            }
        }

        while (!isEnd)
        {
            string value = pathArray[startRow, startColumn];
            energizedArray[startRow, startColumn] = 1;

            if (value.Equals("-"))
            {
                if (direction == DirectionEnum.EAST)
                {
                    if (startColumn + 1 < columnCount)
                    {
                        startColumn = startColumn + 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.WEST)
                {
                    if (startColumn - 1 >= 0)
                    {
                        startColumn = startColumn - 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.NORTH || direction == DirectionEnum.SOUTH)
                {
                    isEnd = true;
                    if (startColumn + 1 < columnCount)
                    {
                        int newStartColumn = startColumn + 1;
                        GoLightGo(startRow, newStartColumn, DirectionEnum.EAST);
                    }

                    if (startColumn - 1 >= 0)
                    {
                        int newStartColumn = startColumn - 1;
                        GoLightGo(startRow, newStartColumn, DirectionEnum.WEST);
                    }
                }
            }
            else if (value.Equals("|"))
            {
                if (direction == DirectionEnum.EAST || direction == DirectionEnum.WEST)
                {
                    isEnd = true;
                    if (startRow + 1 < rowCount)
                    {
                        int newStartRow = startRow + 1;
                        GoLightGo(newStartRow, startColumn, DirectionEnum.SOUTH);
                    }

                    if (startRow - 1 >= 0)
                    {
                        int newStartRow = startRow;
                        GoLightGo(newStartRow, startColumn, DirectionEnum.NORTH);
                    }
                }
                else if (direction == DirectionEnum.SOUTH)
                {
                    if (startRow + 1 < rowCount)
                    {
                        startRow = startRow + 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.NORTH)
                {
                    if (startRow - 1 >= 0)
                    {
                        startRow = startRow - 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
            }
            else if (value.Equals("/"))
            {
                if (direction == DirectionEnum.EAST)
                {
                    if (startRow - 1 >= 0)
                    {
                        startRow--;
                        direction = DirectionEnum.NORTH;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.WEST)
                {
                    if (startRow + 1 < rowCount)
                    {
                        startRow++;
                        direction = DirectionEnum.SOUTH;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.NORTH)
                {
                    if (startColumn + 1 < columnCount - 1)
                    {
                        startColumn++;
                        direction = DirectionEnum.EAST;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.SOUTH)
                {
                    if (startRow - 1 >= 0)
                    {
                        startColumn--;
                        direction = DirectionEnum.WEST;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
            }
            else if (value.Equals(@"\"))
            {
                if (direction == DirectionEnum.EAST)
                {
                    if (startRow + 1 < rowCount)
                    {
                        startRow++;
                        direction = DirectionEnum.SOUTH;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.WEST)
                {
                    if (startRow - 1 >= 0)
                    {
                        startRow--;
                        direction = DirectionEnum.NORTH;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.NORTH)
                {
                    if (startColumn - 1 >= 0)
                    {
                        startColumn--;
                        direction = DirectionEnum.WEST;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.SOUTH)
                {
                    if (startColumn + 1 < columnCount)
                    {
                        startColumn++;
                        direction = DirectionEnum.EAST;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
            }
            else
            {
                if (direction == DirectionEnum.EAST)
                {
                    if (startColumn + 1 < columnCount)
                    {
                        startColumn = startColumn + 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.WEST)
                {
                    if (startColumn - 1 >= 0)
                    {
                        startColumn = startColumn - 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.SOUTH)
                {
                    if (startRow + 1 < rowCount)
                    {
                        startRow = startRow + 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
                else if (direction == DirectionEnum.NORTH)
                {
                    if (startRow - 1 >= 0)
                    {
                        startRow = startRow - 1;
                    }
                    else
                    {
                        isEnd = true;
                    }
                }
            }
        }
    }
}

public enum DirectionEnum
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}