var inputData = ReadData(args.Length >= 1 ? args[0] : "input.txt");
var analysis = inputData.Select(IsRowSafe).ToList();

Console.WriteLine($"Safe Report count: {analysis.Count(i => i)}");

List<List<int>> ReadData(string filename)
{
    var list = new List<List<int>>();
    using var stream = new FileStream(filename, FileMode.Open);
    using var reader = new StreamReader(stream);
    string? line = null;
    while ((line = reader.ReadLine()) != null)
    {
        list.Add(line.Split(' ').Select(int.Parse).ToList());
    }

    return list;
}

bool IsRowSafe(List<int> row)
{
    bool? isIncreasing = null;
    for (var i = 0; i < row.Count - 1; i++)
    {
        var a = row[i];
        var b = row[i + 1];
        if (a == b) return false;
        
        isIncreasing ??= a < b;
        if ((isIncreasing?.Equals(true) ?? false) && b < a) return false;
        if ((isIncreasing?.Equals(false) ?? false) && b > a) return false;
        
        var difference = Math.Abs(a - b);
        if (difference < 1 || difference > 3) return false;
    }
    
    return true;
}