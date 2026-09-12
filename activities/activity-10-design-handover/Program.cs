var stock = new StockService(10);
stock.Remove(3);
stock.Add(2);
Directory.CreateDirectory("output");
File.WriteAllText("output/handover.txt", "R-STOCK-01 -> StockService.Remove -> insufficient-stock test. Component: StockService. Contract: count >= 0. Dependencies: .NET 10 BCL only. Limitation: in-memory, single process, no user authorization. Design decision: encapsulate mutation behind methods; reject unrestricted public setter.");
if (args.Contains("--self-test"))
{
    Check(stock.Count == 9, "normal workflow");
    try
    {
        stock.Remove(10);
        throw new Exception("oversell accepted");
    }
    catch (InvalidOperationException) { }
    Check(stock.Count == 9, "rejected change atomic");
    try
    {
        stock.Add(-1);
        throw new Exception("negative accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    Check(File.ReadAllText("output/handover.txt").Contains("R-STOCK-01"), "handover trace");
    Console.WriteLine("PASS: 5 stock handover checks");
    return;
}
Console.WriteLine($"Stock after remove/add: {stock.Count}");
Console.WriteLine("Handover: output/handover.txt");
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
/// <summary>Reusable in-memory stock component that preserves R-STOCK-01.</summary>
sealed class StockService
{
    /// <summary>Gets the current non-negative quantity.</summary>
    public int Count
    {
        get; private set;
    }
    /// <summary>Creates a component with validated initial stock.</summary>
    /// <param name="initial">Non-negative starting quantity.</param>
    public StockService(int initial)
    {
        if (initial < 0)
            throw new ArgumentOutOfRangeException(nameof(initial));
        Count = initial;
    }
    /// <summary>Adds stock without allowing integer overflow.</summary>
    /// <param name="quantity">Non-negative quantity to add.</param>
    public void Add(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));
        Count = checked(Count + quantity);
    }
    /// <summary>Removes stock atomically; rejected changes preserve the count.</summary>
    /// <param name="quantity">Non-negative quantity to remove.</param>
    /// <exception cref="InvalidOperationException">Insufficient stock.</exception>
    public void Remove(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));
        if (quantity > Count)
            throw new InvalidOperationException("Insufficient stock");
        Count -= quantity;
    }
}
