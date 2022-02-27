using System.ComponentModel.DataAnnotations;

namespace invoice.Entities;

public class Invoice
{

    [Key]
   
    public int Id { get; set; }


    public int Ord_Id { get; set; }

    public virtual Order Order { get; set; }

    public ICollection<Payment> Payments { get; set; }


    [Range(8,2)]

    public decimal Amount { get; set; }


    public DateTimeOffset Issued { get; set; }


    public DateTimeOffset Due { get; set; }



    [Obsolete("Used only for entity binding.", true)]
  
    public Invoice() { }
    
  
    public Invoice(int id, int ord_Id ,decimal amount)
    {
        Id = id;
        Ord_Id = ord_Id;
        Amount = amount;
        Issued = DateTimeOffset.UtcNow;
        Due = Issued;
    }
    
    
}