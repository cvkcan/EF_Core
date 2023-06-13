using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

Console.WriteLine("Hello, World!");

#region Veri Nasıl Güncellenir?

//ECommerceDbContext context = new();
//Product product = await context.Products.FirstOrDefaultAsync(x=>x.Id==1);
//product.LastName = "Ess";
//product.FirstName = "Ozer";
//await context.SaveChangesAsync();

#endregion
#region ChangeTracker Nedir? Kısaca!

//ChangeTracker, context üzerinden gelen verilerin takibinden sorunlu bir mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır!

#endregion
#region Takip Edilemeyen Nesneler Nasıl Güncellenir?
//Product Product = new Product {
//    Id = 1,
//    LastName ="Suda",
//    FirstName="Kuda"
//};
#endregion
#region Update Fonksiyonu

//ECommerceDbContext context = new ECommerceDbContext();
//context.Products.Update(Product);
//await context.SaveChangesAsync();

//ChangeTracker mekanizması tarafından takip edilemeyen nesnelerin güncellenebilmesi için Update fonkisyonu kullanılır!
//Update fonksiyonunu kullanbilmek içindir kesinlikle ilgili nesnede Id değeri verilmelidir! Bu değer güncellenecek (update sorgusu oluşturulacak) verinin hangisi olduğunu ifade edecektir.
#endregion
#region EntitySate Nedir?

//Bir entity instance'inin durumunu ifade eden bir referanstır.

//ECommerceDbContext context= new ECommerceDbContext();
//Product product= new Product();
//Console.WriteLine(context.Entry(product).State);
#endregion
#region EF Core Açısından Bir Verinin Güncellenmesi Gerektiği Nasıl Anlaşılıyor?

//ECommerceDbContext context= new ECommerceDbContext();
//Product product = await context.Products.FirstOrDefaultAsync(x => x.Id == 1 );
//Console.WriteLine(context.Entry(product).State);
//product.LastName = "Product A";
//Console.WriteLine(context.Entry(product).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product).State);

#endregion
#region Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?

//ECommerceDbContext context = new ECommerceDbContext();
//var products = await context.Products.ToListAsync();
//foreach (var product in products)
//{
//    product.LastName += "I";
//}
//await context.SaveChangesAsync();

//Transaction oluşturulacağı için foreach içerisinde değil de dışarısında tanımlamamız lazım. 

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
    public string FirstName { get; set; }
    public string LastName { get; set; }
}