using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace invoice.Entities;

public class Customer
{

     [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }


    [Column(TypeName = "nvarchar(14)")] 
    public string Name { get; set; }

   
    [Column(TypeName = "nchar(3)")] 
    public string Country { get; set; }

   
    public string Text { get; set; }


    [Column(TypeName = "nvarchar(50)")]

    public string Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

   
   
   
   
    [Obsolete("Used only for entity binding.", true)]
    public Customer() { }


    public Customer(int id, string phone,string name="", string country="", string text="")
    {
        Id = id;
        Name = name;
        Country = country;
        Text = text;
        Phone = phone;
    }
    
    
}