using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace invoice.Entities;

public class Product
{

     [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

    public int Id { get; set; }

 
 
    [Column(TypeName = "nvarchar(10)")] 


    public string Name { get; set; }



    public int Category_Id { get; set; }

    public virtual Category  Category { get; set; }


   
    [Column(TypeName = "nvarchar(20)")] 


    public string Description { get; set; }


    
    [Range(6,2)]


    public double Price { get; set; }

    
  
    [Column(TypeName = "nvarchar(1024)")] 

    public string Photo { get; set; }



    [Obsolete("Used only for entity binding.", true)]
    public Product() { }

    
    public Product(int id, string name, int category_Id, double price, string photo , string description="")
    {
        Id = id;
        Name = name;
        Category_Id = category_Id;
        Description = description;
        Price = price;
        Photo = photo;
    }
    
    
    
}