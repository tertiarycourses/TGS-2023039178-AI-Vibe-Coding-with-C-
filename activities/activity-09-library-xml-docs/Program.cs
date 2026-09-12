using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var library = new Library();
library.RegisterMember("M001");
library.LoanBook("B001", "M001");
var fine = FineCalculator.Calculate(new DateOnly(2026, 9, 10), new DateOnly(2026, 9, 13));
Directory.CreateDirectory("output");
File.WriteAllText("output/design-report.txt", "R1 Search -> Library.Search; R2 Register -> RegisterMember; R3 Loan -> LoanBook; R4 Fine -> FineCalculator.Calculate; Dependencies: .NET 10 BCL, no third-party NuGet packages.");
if (args.Contains("--self-test"))
{
    Check(library.Search("c#").Count == 1, "search");
    Check(fine == 1.50m, "three overdue days");
    Check(FineCalculator.Calculate(new(2026, 9, 13), new(2026, 9, 10)) == 0m, "early return");
    try
    {
        library.LoanBook("B002", "missing");
        throw new Exception("member accepted");
    }
    catch (ArgumentException) { }
    Check(File.ReadAllText("output/design-report.txt").Contains("R4 Fine"), "trace report");
    Console.WriteLine("PASS: 5 library documentation checks");
    return;
}
Console.WriteLine($"Search matches: {library.Search("c#").Count}");
Console.WriteLine($"Fine: {fine:F2}");
Console.WriteLine("Report: output/design-report.txt");
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
/// <summary>Immutable searchable book identity (R1).</summary>
sealed record Book(string Id, string Title);
/// <summary>In-memory library component for R1–R3. Data do not persist across runs.</summary>
sealed class Library
{
    private readonly List<Book> books = [new("B001", "C# Design"), new("B002", "Software Tests")];
    private readonly HashSet<string> members = []; private readonly HashSet<string> loans = [];
    /// <summary>Finds books by a case-insensitive title substring (R1).</summary>
    /// <param name="keyword">Search term.</param><returns>Matching books.</returns>
    public List<Book> Search(string keyword) => books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    /// <summary>Registers a non-empty unique member identifier (R2).</summary>
    /// <param name="id">Synthetic member ID.</param>
    public void RegisterMember(string id)
    {
        if (string.IsNullOrWhiteSpace(id) || !members.Add(id))
            throw new ArgumentException("Invalid or duplicate member");
    }
    /// <summary>Loans an available known book to a registered member (R3).</summary>
    /// <param name="bookId">Known book ID.</param><param name="memberId">Registered member ID.</param>
    public void LoanBook(string bookId, string memberId)
    {
        if (!members.Contains(memberId) || !books.Any(b => b.Id == bookId) || !loans.Add(bookId))
            throw new ArgumentException("Invalid member/book or already loaned");
    }
}
/// <summary>Pure calendar-day fine calculation (R4).</summary>
static class FineCalculator
{
    /// <summary>Charges 0.50 per overdue calendar day, never negative (R4).</summary>
    /// <param name="due">Due date.</param><param name="returned">Actual return date.</param><returns>Fine in SGD.</returns>
    public static decimal Calculate(DateOnly due, DateOnly returned) => Math.Max(0, returned.DayNumber - due.DayNumber) * 0.50m;
}
