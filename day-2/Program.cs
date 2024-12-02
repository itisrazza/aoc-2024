using System.Net.Http.Headers;

var inputData = ReadData(args.Length >= 1 ? args[0] : "input.txt");
Console.WriteLine($"{inputData.Count} lines read");

var strictAnalysis = inputData.Select(IsRowSafe).ToList();
Console.WriteLine($"Strict Safe Report count: {strictAnalysis.Count(i => i)}");

var dampenedAnalysis = inputData.Select(DampenedIsRowSafe).ToList();
Console.WriteLine($"Dampened Safe Report count: {dampenedAnalysis.Count(i => i)}");

List<List<int>> ReadData(string filename)
{
    var list = new List<List<int>>();
    using var stream = new FileStream(filename, FileMode.Open);
    using var reader = new StreamReader(stream);
    while (reader.ReadLine() is { } line)
    {
        list.Add(line.Split(' ').Select(int.Parse).ToList());
    }

    return list;
}

bool IsStepSafe(int a, int b, ref bool? isIncreasing)
{
    if (a == b) return false;

    isIncreasing ??= a < b;
    if ((isIncreasing?.Equals(true) ?? false) && b < a) return false;
    if ((isIncreasing?.Equals(false) ?? false) && b > a) return false;

    var difference = Math.Abs(a - b);
    return difference is >= 1 and <= 3;
}

bool IsRowSafe(List<int> row)
{
    if (row.Count == 0) throw new IndexOutOfRangeException();
    
    bool? isIncreasing = null;
    for (var i = 0; i < row.Count - 1; i++)
    {
        var a = row[i];
        var b = row[i + 1];
        if (!IsStepSafe(a, b, ref isIncreasing)) return false;
    }

    return true;
}

bool DampenedIsRowSafe(List<int> row)
{
    if (IsRowSafe(row)) return true;

    for (var i = 0; i < row.Count; i++)
    {
        var rowWithRemoved = new List<int>(row);
        if (i >= 0) rowWithRemoved.RemoveAt(i);

        if (IsRowSafe(rowWithRemoved)) return true; 
    }

    return false;
}