using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var standard = new GradedStudent("Ada");
standard.AddGrade(90);
standard.AddGrade(80);
var honours = new HonoursStudent("Lin");
honours.AddGrade(90);
honours.AddGrade(80);
if (args.Contains("--self-test"))
{
    Check(standard.CalculateAverage() == 85m, "mean");
    Check(standard.GetGradeBand() == "B", "standard band");
    Check(((IGradable)honours).GetGradeBand() == "A", "virtual dispatch");
    try
    {
        standard.AddGrade(101);
        throw new Exception("invalid accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    try
    {
        new GradedStudent("Empty").CalculateAverage();
        throw new Exception("empty accepted");
    }
    catch (InvalidOperationException) { }
    Console.WriteLine("PASS: 5 grade checks");
    return;
}
foreach (IGradable student in new IGradable[] { standard, honours }) Console.WriteLine($"Mean {student.CalculateAverage():F2}; band {student.GetGradeBand()}");
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
interface IGradable
{
    decimal CalculateAverage(); string GetGradeBand();
}
abstract class Student(string name)
{
    private readonly List<decimal> grades = []; public string Name { get; } = name;
    protected IReadOnlyList<decimal> Grades => grades.AsReadOnly();
    public void AddGrade(decimal grade)
    {
        if (grade < 0 || grade > 100)
            throw new ArgumentOutOfRangeException(nameof(grade));
        grades.Add(grade);
    }
}
class GradedStudent(string name) : Student(name), IGradable
{
    public decimal CalculateAverage() => Grades.Count == 0 ? throw new InvalidOperationException("No grades") : Grades.Average();
    public virtual string GetGradeBand() => CalculateAverage() switch { >= 90 => "A", >= 80 => "B", >= 70 => "C", >= 60 => "D", _ => "F" };
}
sealed class HonoursStudent(string name) : GradedStudent(name)
{
    public override string GetGradeBand() => CalculateAverage() switch { >= 85 => "A", >= 75 => "B", >= 65 => "C", >= 55 => "D", _ => "F" };
}
