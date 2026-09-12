using System.Text.Json;
using System.Text.RegularExpressions;
Directory.CreateDirectory("output");
const string sample = """{"Sku":"SKU-001","Name":"Keyboard","Quantity":10}""";
var item = Import(sample);
string path = Path.Combine("output", "product.json");
using (var writer = new StreamWriter(path, false, System.Text.Encoding.UTF8)) writer.Write(JsonSerializer.Serialize(item));
var reloaded = Import(File.ReadAllText(path));
if (args.Contains("--self-test"))
{
    Check(reloaded == item, "round trip");
    Check(Import("""{"Sku":"SKU-002","Name":"米","Quantity":0}""").Name == "米", "unicode zero stock");
    ExpectInvalid("""{"Sku":"SKU-٠٠١","Name":"Rice","Quantity":1}""");
    ExpectInvalid("""{"Sku":"bad","Name":"Rice","Quantity":1}""");
    ExpectInvalid("""{"Sku":"SKU-003","Name":"Rice","Quantity":-1}""");
    try
    {
        Import("""{"Sku":"SKU-003","Name":"Rice","Quantity":"ten"}""");
        throw new Exception("type accepted");
    }
    catch (JsonException) { }
    Console.WriteLine("PASS: 5 JSON contract checks");
    return;
}
Console.WriteLine($"Imported {item.Sku}; stock {item.Quantity}");
Console.WriteLine($"Round-trip equal: {reloaded == item}");
Console.WriteLine("Export: output/product.json");
static ProductDto Import(string json)
{
    var p = JsonSerializer.Deserialize<ProductDto>(json) ?? throw new ArgumentException("JSON null");
    if (!Regex.IsMatch(p.Sku ?? "", @"\ASKU-[0-9]{3}\z", RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100)) || string.IsNullOrWhiteSpace(p.Name) || p.Quantity < 0)
        throw new ArgumentException("Invalid product contract");
    return p;
}
static void ExpectInvalid(string json) { try { Import(json); throw new Exception("invalid accepted"); } catch (ArgumentException) { } }
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
sealed record ProductDto(string Sku, string Name, int Quantity);
