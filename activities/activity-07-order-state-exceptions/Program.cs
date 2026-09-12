Directory.CreateDirectory("output");
string logPath = Path.Combine("output", "order-log.txt");
File.WriteAllText(logPath, "");
var processor = new OrderProcessor(new PaymentService(), new NotificationService(), logPath);
var ok = processor.Process("ORDER-001", true, false, false);
var fail = processor.Process("ORDER-002", false, false, false);
if (args.Contains("--self-test"))
{
    Check(ok.Status == OrderStatus.Confirmed && ok.Notified, "success");
    Check(fail.Status == OrderStatus.PaymentFailed && !fail.Notified, "rejection");
    var cancelled = processor.Process("ORDER-003", true, true, false);
    Check(cancelled.Status == OrderStatus.Cancelled && !cancelled.Notified, "cancelled");
    var broken = processor.Process("ORDER-004", true, false, true);
    Check(broken.Status == OrderStatus.PaymentFailed && !broken.Notified, "exception");
    Check(File.ReadAllText(logPath).Contains("ORDER-004: payment exception"), "log evidence");
    Console.WriteLine("PASS: 5 order integration checks");
    return;
}
Console.WriteLine($"ORDER-001: {ok.Status}; notified {ok.Notified}");
Console.WriteLine($"ORDER-002: {fail.Status}; notified {fail.Notified}");
Console.WriteLine("Log: output/order-log.txt");
static void Check(bool condition, string label) { if (!condition) throw new InvalidOperationException(label); }
enum OrderStatus
{
    Pending, PaymentProcessing, PaymentSuccessful, PaymentFailed, Confirmed, Cancelled
}
readonly record struct OrderResult(OrderStatus Status, bool Notified);
sealed class PaymentService
{
    public bool Pay(bool approve, bool throwError)
    {
        if (throwError)
            throw new InvalidOperationException("Mock payment unavailable");
        return approve;
    }
}
sealed class NotificationService
{
    public bool Send(OrderStatus status) => status == OrderStatus.Confirmed;
}
sealed class OrderProcessor(PaymentService payment, NotificationService notification, string logPath)
{
    public OrderResult Process(string id, bool approve, bool cancelled, bool throwError)
    {
        OrderStatus status = OrderStatus.Pending;
        if (cancelled)
        {
            status = OrderStatus.Cancelled;
            Log(id, status.ToString());
            return new(status, false);
        }
        status = OrderStatus.PaymentProcessing;
        Log(id, status.ToString());
        try
        {
            if (!payment.Pay(approve, throwError))
            {
                status = OrderStatus.PaymentFailed;
                Log(id, status.ToString());
                return new(status, false);
            }
            status = OrderStatus.PaymentSuccessful;
            Log(id, status.ToString());
            status = OrderStatus.Confirmed;
            bool sent = notification.Send(status);
            Log(id, $"{status}; notified {sent}");
            return new(status, sent);
        }
        catch (InvalidOperationException) { Log(id, "payment exception"); return new(OrderStatus.PaymentFailed, false); }
    }
    private void Log(string id, string message) => File.AppendAllText(logPath, $"{id}: {message}{Environment.NewLine}");
}
