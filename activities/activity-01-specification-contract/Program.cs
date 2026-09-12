using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
if (args.Contains("--self-test"))
{
    Check(Validate("Keyboard", 49.90m, 10, "Electronics") is null, "valid product");
    Check(Validate("", 10m, 1, "Food") == "name", "empty name");
    Check(Validate("Rice", 0m, 1, "Food") == "price", "zero price");
    Check(Validate("Rice", 2m, -1, "Food") == "stock", "negative stock");
    Check(Validate("Rice", 2m, 1, "Other") == "category", "unknown category");
    Console.WriteLine("PASS: 5 contract checks");
    return;
}
Console.WriteLine($"Valid catalogue record: {Validate("Keyboard", 49.90m, 10, "Electronics") is null}");
Console.WriteLine($"Invalid price -> {Validate("Rice", 0m, 1, "Food")}");
static string? Validate(string? name, decimal price, int stock, string category)
{
    if (string.IsNullOrWhiteSpace(name))
        return "name";
    if (price <= 0)
        return "price";
    if (stock < 0)
        return "stock";
    if (category is not ("Electronics" or "Clothing" or "Food"))
        return "category";
    return null;
}
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
