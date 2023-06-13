using Microsoft.EntityFrameworkCore;
using System.Net;

Console.WriteLine("Hello, World!");

public class ECommerceDbContext: DbContext
{
    public DbSet<Product> Products  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ECommerceDb; User Id=sa; Password=Windowserver19*");

        //Provider,
        //ConnectionString,
        //Lazy Loading,
        //vb configurations
    }
}

public class Product
{
    public int Id { get; set; }
    
    // Basic aşamada default olarak Id içeren (tabloadiID,tabloadiId,Id,ID gibi) primary key olarak alır.
    // PK olmadan entity olmaz.
}