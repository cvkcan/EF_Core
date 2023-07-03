using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("A");

#region Default Convention
//İki entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız. (ICollection, List)

//Defaılt Convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Core tasarıma uygun bir şekilde cross table'ın içerisinde Composite Primary Key'i otomatik oluşturmuş olacaktır.

//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}

#endregion

#region Data Annotations
//Cross Table manuel olarak oluşturulmak zorundadır.

//Entity'lerde oluşturduğumuz cross table entity'si ile bire çok bir ilişki kurulmalı.

//CT'da Composite Primary Key'i Data Annotations (Attributes)lar ile manuel kuramıyoruz. Bunun için de Fluent API'da çalışma yapmamız gerekiyor.

//Cross Table'a karşılık bir entity modeli oluşturuyorsak eğer bunu context sınıfı içerisinde DBSet Property'si olarak bildirmek mecburiyetinde değiliz!

//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar{ get; set; }
//}

//class KitapYazar //Cross Table
//{

//    public int KitapId { get; set; }

//    //[ForeignKey(nameof(Kitap))]
//    //public int KId { get; set; }

//    public int YazarId{ get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }
//}
#endregion

#region Fluent API
//Cross Table manuel oluşturulmaldıır.

//DbSet olarak eklenmesine lüzum yoktur.

//Composite PK HasKey metodu ile kurulmalııdr.  
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdı { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}

//class KitapYazar
//{
//    public int KitapId { get; set; }
//    public int YazarId { get; set; }
//    public Yazar Yazar { get; set; }
//    public Kitap Kitap { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }
//}

#endregion
class EBookDbContext :DbContext
{
    public DbSet<Yazar> Yazarlar { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ECommerceDb; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }

    #region Data Annotations Yapısı İçin

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky=> new { ky.KitapId, ky.YazarId });
    //}

    #endregion

    #region Fluent API Yapısı İçin

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky=> new { ky.KitapId,ky.YazarId});

    //    modelBuilder.Entity<KitapYazar>()
    //        .HasOne(ky => ky.Kitap)
    //        .WithMany(ky => ky.Yazarlar)
    //        .HasForeignKey(ky=>ky.KitapId);

    //    modelBuilder.Entity<KitapYazar>()
    //        .HasOne(ky => ky.Yazar)
    //        .WithMany(ky => ky.Kitaplar)
    //        .HasForeignKey(ky=>ky.YazarId);
    //}

    #endregion
}