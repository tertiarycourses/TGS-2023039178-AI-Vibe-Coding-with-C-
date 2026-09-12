using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var account = new BankAccount("DEMO-001", 125.50m);
var view = new BalanceView();
var controller = new BalanceController(account, view);
if (args.Contains("--self-test"))
{
    Check(controller.Enquire() == "DEMO-001: balance 125.50", "MVC result");
    Check(Factorial(0) == 1 && Factorial(5) == 120, "recursion base and result");
    try
    {
        new BankAccount("bad", -1);
        throw new Exception("negative accepted");
    }
    catch (ArgumentOutOfRangeException) { }
    Console.WriteLine("PASS: MVC and method checks");
    return;
}
Console.WriteLine(controller.Enquire());
Console.WriteLine($"Factorial(5): {Factorial(5)}");
static int Factorial(int n) { if (n < 0 || n > 12) throw new ArgumentOutOfRangeException(nameof(n)); return n <= 1 ? 1 : checked(n * Factorial(n - 1)); }
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
sealed class BankAccount
{
    private readonly decimal balance;
    public string AccountNumber
    {
        get;
    }
    public decimal Balance => balance;
    public BankAccount(string accountNumber, decimal initialBalance)
    {
        if (initialBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(initialBalance));
        AccountNumber = accountNumber;
        balance = initialBalance;
    }
}
sealed class BalanceView
{
    public string Format(string number, decimal balance) => $"{number}: balance {balance:F2}";
}
sealed class BalanceController(BankAccount model, BalanceView view)
{
    public string Enquire() => view.Format(model.AccountNumber, model.Balance);
}
