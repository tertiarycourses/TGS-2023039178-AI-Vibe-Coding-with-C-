if (args.Contains("--self-test"))
{
    Check(Greet("Ada") == "Welcome, Ada!", "named user");
    Check(Greet(null) == "Welcome, learner!", "null input");
    Check(Greet("   ") == "Welcome, learner!", "blank input");
    Console.WriteLine("PASS: 3 greeting checks");
    return;
}
Console.WriteLine(Greet("Ada"));
Console.WriteLine(Greet(null));
static string Greet(string? name) => $"Welcome, {(string.IsNullOrWhiteSpace(name) ? "learner" : name.Trim())}!";
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
