// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

#region Veri Nasıl Eklenir?

ECommerceDbContext context = new();
Product product = new Product
{
    Price = 5000,
    ProductName = "Test"

};
Product product2 = new Product
{
    Price = 6000,
    ProductName = "Product A"
};
#endregion

#region context.AddAsync Fonksiyonu

//await context.AddAsync(product);
//await context.SaveChangesAsync();

#endregion

#region context.DbSet.AddAsync Fonksiyonu

//await context.Products.AddAsync(product2);
//await context.SaveChangesAsync();

#endregion

#region Çoklu Veri Ekleme

//await context.AddRangeAsync(product, product2);
//await context.SaveChangesAsync();

//await context.Products.AddRangeAsync(product,product2);
//await context.SaveChangesAsync();

#endregion

#region SaveChangs'i Verimli Kullanma!

// SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolayı EF Core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme özel transaction veritabanı açısından ekstradan maliyet demektir. O yüzden mümkün mertebe tüm işlemerimizi tek bir transcation eşliğinde veritabanına gönderebilmek için savechanges'i aşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilirlik açısından katkı sağlayacaktır.

await context.Products.AddAsync(product);
await context.Products.AddAsync(product2);
await context.SaveChangesAsync();

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
    public string ProductName { get; set; }
    public int Price { get; set; }
}