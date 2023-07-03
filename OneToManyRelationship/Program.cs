using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

ECommerceDbContext context = new();

#region Defaut Convention
//Default Convention'da One To Many ilişkilerde Foreign Key belirtmesek te olur. Sistem otomatik olarak Foreign Key tanımlayacaktır.

//Default Convention yönteminde bire çok ilişkii kurarken Foreign Key kolonuna karşılık gelen bir property tanımlamak mecburiyetinde değildir. Eğer tanımlamazsak EF Core bunu kendisi tamamlayacaktır, eğer ki tanımlarsak da tanımladığımız baz alır.

//public class Calisan //Dependenty Entity
//{
//    public int Id { get; set; }
//    public int DepartmanId { get; set; } //Opsiyonel
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//public class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region Data Annotations
//Default convention yönteminde foreign key kolonunna karşılık gelen property'i tanımladığımızda bu property ismi temel geleneksel entity tanımlama kurallarına uymuyorsa eğer Data Annotations'lar ile müdahalede bulunabiliriz.
//public class Calisan //Dependenty Entity
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }
//}

//public class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion

#region Fluent API
//Farklı bir foreign key isimlendirmesi yapılacak ise OnModelCreating fonksiyonunuda bunu belirtmemiz gerekir. 
public class Calisan //Dependenty Entity
{
    public int Id { get; set; }
    public int DId { get; set; }
    public string Adi { get; set; }
    public Departman Departman { get; set; }
}

public class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
#endregion

public class ECommerceDbContext:DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ECommerceDb; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }

    #region Fluent API Kısmı

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(c => c.Calisanlar)
            .HasForeignKey(c => c.DId);

        //modelBuilder.Entity<Departman>()
        //    .HasMany(d=>d.Calisanlar)
        //    .WithOne(c=>c.Departman);
    }

    #endregion

}
