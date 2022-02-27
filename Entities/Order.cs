using System.ComponentModel.DataAnnotations;


namespace invoice.Entities;

public class Order
{

    [Key]

    public int Id { get; set; }

    
    public DateTimeOffset Date { get; set; }


    public int Cust_Id { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; }

    
    
    [Obsolete("Used only for entity binding.", true)]
    public Order() { }
    
    public Order(int id, int cust_id)
    {
        Id = id;
        Date = DateTimeOffset.UtcNow;
        Cust_Id = cust_id;
    }
     
    
}