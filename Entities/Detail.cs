using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.Entities;

public class Detail
{

     [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

    public int Id { get; set; }

    public int Ord_Id { get; set; }

    public virtual Order Order { get; set; }

    public int Pr_Id { get; set; }

    public virtual Product Product { get; set; }

    public short Quantity { get; set; }


    [Obsolete("Used only for entity binding.", true)]
  
    public Detail() { }
    
    
    
    public Detail(int id, int ord_Id, int pr_Id, short quantity)
    {
        Id = id;
        Ord_Id = ord_Id;
        Pr_Id = pr_Id;
        Quantity = quantity;
    }
}