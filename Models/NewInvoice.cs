namespace invoice.Models;

public class NewInvoice
{
    public int Id { get; set; }


    public int Ord_Id { get; set; }


    public decimal Amount { get; set; }


    public DateTimeOffset Issued { get; set; }


    public DateTimeOffset Due { get; set; }

}