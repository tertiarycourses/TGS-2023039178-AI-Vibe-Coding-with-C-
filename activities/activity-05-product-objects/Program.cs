using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var catalogue = new ProductCatalogue();
catalogue.Add(new Product("Keyboard", 49.90m, 10, ProductCategory.Electronics));
if (args.Contains("--self-test"))
{
    Check(catalogue.Count == 1, "add count");
    try
    {
        new Product("", 1m, 1, ProductCategory.Food);
        throw new Exception("blank accepted");
    }
    catch (ArgumentException) { }
    try
    {
        new Product("Rice", 0, 1, ProductCategory.Food);
        throw new Exception("zero accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    try
    {
        new Product("Rice", 2, 1, (ProductCategory)99);
        throw new Exception("category accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    var p = new Product("Rice", 2, 0, ProductCategory.Food);
    Check(p.Stock == 0, "zero stock");
    Console.WriteLine("PASS: 5 catalogue checks");
    return;
}
Console.WriteLine($"Added Keyboard; catalogue count: {catalogue.Count}");
Console.WriteLine(catalogue.Items[0].Describe());
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
enum ProductCategory
{
    Electronics, Clothing, Food
}
sealed class Product
{
    public string Name
    {
        get;
    }
    public decimal Price
    {
        get;
    }
    public int Stock
    {
        get; private set;
    }
    public ProductCategory Category
    {
        get;
    }
    public Product(string name, decimal price, int stock, ProductCategory category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name required", nameof(name));
        if (price <= 0 || stock < 0 || !Enum.IsDefined(category))
            throw new ArgumentOutOfRangeException(nameof(price));
        Name = name.Trim();
        Price = price;
        Stock = stock;
        Category = category;
    }
    public void AdjustStock(int delta)
    {
        int next = checked(Stock + delta);
        if (next < 0)
            throw new InvalidOperationException("Insufficient stock");
        Stock = next;
    }
    public string Describe() => $"{Name} | {Category} | {Price:F2} | stock {Stock}";
}
sealed class ProductCatalogue
{
    private readonly List<Product> products = [];
    public IReadOnlyList<Product> Items => products.AsReadOnly(); public int Count => products.Count;
    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        products.Add(product);
    }
}
static class TestSupport
{
}
