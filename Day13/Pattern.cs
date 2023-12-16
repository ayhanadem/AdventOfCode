using System.Data;
using System.Text;

namespace Day13;

public class Pattern
{
    public string[,] Values { get; set; }
    public List<PatternRow> Rows { get; set; } = new();
    public List<PatternColumn> Columns { get; set; } = new();

    public int Score { get; set; }

    public Pattern(List<List<string>> initializeValue)
    {
        var rowCount = initializeValue.Count;
        var columnCount = initializeValue[0].Count;


        Values = new string[rowCount, columnCount];

        for (int i = 0; i < initializeValue.Count; i++)
        {
            var row = initializeValue[i];
            for (int j = 0; j < row.Count; j++)
            {
                Values[i, j] = row[j];
            }
        }

        for (int i = 0; i < rowCount; i++)
        {
            var inRow = new PatternRow();
            inRow.RowNumber = i + 1;
            var sb = new StringBuilder();
            for (int j = 0; j < columnCount; j++)
            {
                inRow.Values.Add(Values[i, j]);
                sb.Append(Values[i, j]);
            }

            inRow.Code = sb.ToString();

            Rows.Add(inRow);
        }

        for (int i = 0; i < columnCount; i++)
        {
            var inColumn = new PatternColumn();
            inColumn.ColumnNumber = i + 1;
            var sb = new StringBuilder();

            for (int j = 0; j < rowCount; j++)
            {
                inColumn.Values.Add(Values[j, i]);
                sb.Append(Values[j, i]);
            }

            inColumn.Code = sb.ToString();

            Columns.Add(inColumn);
        }


        CalculateMirrors();
    }

    private void CalculateMirrors()
    {
        var dictRows = new Dictionary<string, List<int>>();
        var dictColumns = new Dictionary<string, List<int>>();


        foreach (var row in Rows)
        {
            if (dictRows.ContainsKey(row.Code))
            {
                var value = dictRows[row.Code];
                if (!value.Contains(row.RowNumber))
                {
                    value.Add(row.RowNumber);
                    dictRows[row.Code] = value;
                }
            }
            else
            {
                var value = new List<int>();
                value.Add(row.RowNumber);
                dictRows[row.Code] = value;
            }
        }


        foreach (var column in Columns)
        {
            if (dictColumns.ContainsKey(column.Code))
            {
                var value = dictColumns[column.Code];
                if (!value.Contains(column.ColumnNumber))
                {
                    value.Add(column.ColumnNumber);
                    dictColumns[column.Code] = value;
                }
            }
            else
            {
                var value = new List<int>();
                value.Add(column.ColumnNumber);
                dictColumns[column.Code] = value;
            }
        }


        var rowList = new List<int>();
        var columnList = new List<int>();
        foreach (var rowPair in dictRows)
        {
            if (rowPair.Value.Count > 1)
            {
                int value = (int)rowPair.Value.Sum() / (int)rowPair.Value.Count;
                rowList.Add(value);
            }
        }

        foreach (var columnPair in dictColumns)
        {
            if (columnPair.Value.Count > 1)
            {
                int value = (int)columnPair.Value.Sum() / (int)columnPair.Value.Count;
                columnList.Add(value);
            }
        }

        
        rowList = rowList.Distinct().Order().ToList();
        columnList = columnList.Distinct().Order().ToList();

        Score = rowList.FirstOrDefault(0) * 100 + columnList.FirstOrDefault(0);
    }
}

public class PatternRow
{
    public int RowNumber { get; set; }
    public List<string> Values { get; set; } = new List<string>();
    public string Code { get; set; }
    public bool IsMirror { get; set; } = false;
}

public class PatternColumn
{
    public int ColumnNumber { get; set; }
    public List<string> Values { get; set; } = new List<string>();
    public string Code { get; set; }
    public bool IsMirror { get; set; } = false;
}