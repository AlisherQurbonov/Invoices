namespace invoice.Models;

public class NewPayment
{
    public int Id { get; set; }

    public DateTimeOffset Time { get; set; }

    public decimal Amount { get; set; }

    public int Inv_Id { get; set; }


}