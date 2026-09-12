using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
if (args.Contains("--self-test"))
{
    Check(Total(19.90m, 2, 0.10m) == 35.82m, "discount total");
    Check(!int.TryParse("two", out _), "malformed quantity");
    Check(Band(90) == "A" && Band(89) == "B" && Band(0) == "F", "grade boundaries");
    try
    {
        Total(10m, -1, 0m);
        throw new Exception("negative accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    Console.WriteLine("PASS: receipt and grade checks");
    return;
}
Console.WriteLine($"Receipt total: {Total(19.90m, 2, 0.10m):F2}");
int[] grades = [90, 80, 70];
int sum = 0;
foreach (int grade in grades) sum += grade;
Console.WriteLine($"Mean grade: {(decimal)sum / grades.Length:F2}");
Console.WriteLine($"Grade band: {Band(sum / grades.Length)}");
Console.WriteLine($"Parse quantity 'two': {int.TryParse("two", out _)}");
static decimal Total(decimal price, int quantity, decimal discount)
{
    if (price < 0 || quantity < 0 || discount < 0 || discount > 1)
        throw new ArgumentOutOfRangeException(nameof(quantity));
    return decimal.Round(price * quantity * (1 - discount), 2, MidpointRounding.AwayFromZero);
}
static string Band(int grade) => grade switch { >= 90 => "A", >= 80 => "B", >= 70 => "C", >= 60 => "D", _ => "F" };
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
