using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;


#region Veri Nasıl Silimir?

//ECommerceDbContext context = new();
//Product product = await context.Products.FirstOrDefaultAsync(x => x.Id == 1);
//context.Remove(product);
//await context.SaveChangesAsync();

#endregion
#region Silme İşleminde ChangeTracker'ın Rolü

//ChangeTracker, context üzerinden gelen verilerin takibinden sorunlu bir mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır!

#endregion
#region Takip Edilemeyen Nesneler Nasıl Silnir?

//Product product = new Product { 
//    Id=2
//};

//ECommerceDbContext context = new ECommerceDbContext();
//context.Products.Remove(product);
//await context.SaveChangesAsync();


#endregion
#region EntityState İle Silme İşlemi

//ECommerceDbContext context = new();
//Product product = new Product { Id = 3 };
//context.Entry(product).State = EntityState.Deleted;
//await context.SaveChangesAsync();

#endregion
#region RemoveRange

//ECommerceDbContext context = new();
//List<Product> products=await context.Products.Where(x => x.Id >= 8 && x.Id <= 10).ToListAsync();
//context.Products.RemoveRange(products);
//await context.SaveChangesAsync();

#endregion

public class ECommerceDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ECommerceDb; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }
}

public class Product
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string FullName { get; set; }
}