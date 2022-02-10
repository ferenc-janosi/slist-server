namespace slist_server.Data.Models; 
public class ShoppingItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }

    [MaxLength(60)]
    public string Name { get; set; }

    public int Quantity { get; set; }

}
