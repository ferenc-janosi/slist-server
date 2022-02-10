namespace slist_server.Data;
public class SListDbContext : DbContext
{
    public SListDbContext(DbContextOptions<SListDbContext> options): base(options) { }


    // Allgemeine Daten 
    public DbSet<slist_server.Data.Models.ShoppingItem> ShoppingItem { get; set; }


}
