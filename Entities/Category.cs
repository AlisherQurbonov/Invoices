using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace invoice.Entities;

public class Category
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

    public int Id { get; set; }


    [Column(TypeName = "nvarchar(250)")]

   
    public string Name { get; set; }


    public virtual ICollection<Product> Products { get; set; }


   
    [Obsolete("Used only for entity binding.", true)]
    public Category() { }

   
   
    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }

}