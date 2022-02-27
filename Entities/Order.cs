using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace invoice.Entities;

public class Order
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

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
        Date = DateTimeOffset.UtcNow.ToLocalTime();
        Cust_Id = cust_id;
    }
     
    
}