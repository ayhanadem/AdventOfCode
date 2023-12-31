using System.ComponentModel.Design;

namespace Day13;

public class Calculator
{
    public static void ReadFileAndCalculate()
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var path = Path.Combine(Directory.GetCurrentDirectory(),
            "testInput.txt");
        


        var lines = File.ReadAllLines(path);


        var patterns = new List<Pattern>();
        var patternList = new List<List<string>>();

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                var pattern = new Pattern(patternList);
                patterns.Add(pattern);
                patternList = new List<List<string>>();
            }
            else
            {
                var patternLine = new List<string>();
                foreach (var ch in line)
                {
                    patternLine.Add(ch.ToString());
                }

                patternList.Add(patternLine);
            }
        }

        if (patternList.Any())
        {
            var pattern = new Pattern(patternList);
            patterns.Add(pattern);
            patternList = new List<List<string>>();
        }



        int sum = 0;
        foreach (var pattern in patterns)
        {
            sum += pattern.Score;
        }
        Console.WriteLine(sum);
    }
}

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> SplitWhen<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var sublist = new List<T>();
        foreach (var item in source)
        {
            if (predicate(item))
            {
                if (sublist.Any())
                {
                    yield return sublist;
                    sublist = new List<T>();
                }
            }
            else
            {
                sublist.Add(item);
            }
        }

        if (sublist.Any())
        {
            yield return sublist;
        }
    }
}

class Solution
{
    public record Field(char[][] FieldData)
    {
        public int Width => FieldData[0].Length;
        public int Height => FieldData.Length;

        public List<ulong> Horizontal =>
            Enumerable.Range(0, Width).Select(x =>
                    Convert.ToUInt64(new string(Enumerable.Range(0, Height).Select(y => FieldData[y][x]).ToArray()), 2))
                .ToList();

        public List<ulong> Vertical =>
            Enumerable.Range(0, Height).Select(y =>
                    Convert.ToUInt64(
                        new string(Enumerable.Range(0, Width).Select(x => FieldData[y][x]).Reverse().ToArray()), 2))
                .ToList();

        public List<int> HorizontalPalindromes => FindPalindrome(Horizontal);
        public List<int> VerticalPalindromes => FindPalindrome(Vertical);

        public int FirstHorizontalPalindromeIndex => HorizontalPalindromes.Count == 0 ? -1 : HorizontalPalindromes[0];
        public int FirstVerticalPalindromeIndex => VerticalPalindromes.Count == 0 ? -1 : VerticalPalindromes[0];

        public int Score => (FirstVerticalPalindromeIndex + 1) * 100 + FirstHorizontalPalindromeIndex + 1;

        public Field GetAlternative(int x, int y)
        {
            var newFieldData = FieldData.Select(row => row.ToArray()).ToArray();
            newFieldData[y][x] = newFieldData[y][x] == '0' ? '1' : '0';
            return new Field(newFieldData);
        }

        public bool IsPalindrome(List<ulong> list, int index)
        {
            int minLength = Math.Min(index + 1, list.Count - index - 1);
            return list.Skip(index - minLength + 1).Take(minLength)
                .SequenceEqual(list.Skip(index + 1).Take(minLength).Reverse());
        }

        public List<int> FindPalindrome(List<ulong> list) =>
            Enumerable.Range(0, list.Count - 1).Where(i => list[i] == list[i + 1] && IsPalindrome(list, i)).ToList();

        public IEnumerable<Field> FindAlternativeReflections() =>
            Enumerable.Range(0, Width).SelectMany(x => Enumerable.Range(0, Height), (x, y) => GetAlternative(x, y))
                .Where(newField => HasDifferentPalindromes(newField));

        private bool HasDifferentPalindromes(Field newField)
        {
            return (newField.HorizontalPalindromes.Any() &&
                    !newField.HorizontalPalindromes.SequenceEqual(HorizontalPalindromes)) ||
                   (newField.VerticalPalindromes.Any() &&
                    !newField.VerticalPalindromes.SequenceEqual(VerticalPalindromes));
        }

        public int GetAlternativeScore(Field original)
        {
            var newHorizontalPalindromes =
                HorizontalPalindromes.Where(o => !original.HorizontalPalindromes.Contains(o)).ToList();
            var newVerticalPalindromes =
                VerticalPalindromes.Where(o => !original.VerticalPalindromes.Contains(o)).ToList();

            var numCols = newHorizontalPalindromes.Count == 0 ? 0 : newHorizontalPalindromes[0] + 1;
            var numRows = newVerticalPalindromes.Count == 0 ? 0 : newVerticalPalindromes[0] + 1;

            return numRows * 100 + numCols;
        }
    }

    public Field ParseFieldLines(IEnumerable<string> lines) =>
        new(lines.Select(line => line.ToCharArray()).ToArray());

    public List<Field> ParseInput(string input) =>
        input.Replace(".", "0").Replace("#", "1").Split("\n").SplitWhen(line => line.Length == 0)
            .Select(ParseFieldLines).ToList();

    public object PartOne(string input) =>
        ParseInput(input).Sum(field => field.Score);

    public object PartTwo(string input) =>
        ParseInput(input).Sum(field => field.FindAlternativeReflections().First().GetAlternativeScore(field));
}