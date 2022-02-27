using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace invoice.Entities;

public class Payment
{

  [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
   
    public int Id { get; set; }

    public DateTimeOffset Time { get; set; }

    public decimal Amount { get; set; }

    public int Inv_Id { get; set; }

    public virtual Invoice Invoice { get; set; }
   
   
    [Obsolete("Used only for entity binding.", true)]
    public Payment() { }
   
   
    public Payment(int id, decimal amount, int inv_Id)
    {
        Id = id;
        Time = DateTimeOffset.UtcNow;
        Amount = amount;
        Inv_Id = inv_Id;
    }
}