using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");

#region Default Convention
//Her iki entity'de Navigation Property ile birbirlerine tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.

//One To One ilişki türünde, dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir Foreign Key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.

//Böylece Foreign Key'e karşılık property tanımlaayarak lüzumsuz bir kolon oluşturmuş oluyoruz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion

#region Data Annotations
//Navigation Property'ler tanımlanmalıdır.

//Foreign konumunu ismi defaul convention'ın dışında bir kolon olacaksa eğer Foreign Key attribute ile bunu bildirebiliriz.

//Foreign Key kolonu oluşturulmak zorunda değildir.

//1'e 1 ilişkide ekstradan Foreign Key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunu hem Foreign Key hem de Primary Key olarak kullanmayı tercih ediyoruz ve bu duruma özen gösteriyoruz.

//Foreign Key için ekstra unique indexer'ı oluşturulacağından maliyettir.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

////class CalisanAdresi
////{
////    public int Id { get; set; }
////    [ForeignKey(nameof(Calisan))]
////    public int CalisanId { get; set; } // Buradaki CalisanId ismi Herhangi bir şey olabilir.
////    public string Adres { get; set; }
////    public Calisan Calisanlar { get; set; }
////}
//class CalisanAdresi
//{
//    [Key,ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion
#region Fluent API
//Navigation Propertyler tanımlanmalı.

//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonunun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir. Yani tüm sorumluluk bu fonksiyon içerisindeki çalışmalardadır.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}


class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
#endregion

class ESirketDbContext:DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ECommerceDb; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }

    #region Fluent API OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Calisan>()
        //    .HasOne(c => c.CalisanAdresi) // Navigation Property'i gösterdik
        //    .WithOne(c => c.Calisan)
        //    .HasForeignKey<CalisanAdresi>(c => c.Id);

        //modelBuilder.Entity<CalisanAdresi>()
        //    .HasKey(c => c.Id);

        modelBuilder.Entity<CalisanAdresi>()
            .HasOne(c => c.Calisan)
            .WithOne(c => c.CalisanAdresi)
            .HasForeignKey<CalisanAdresi>(c => c.Id);

        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c => c.Id);

        //Yukarıdaki 2 kod aynı özelliğe sahiptir.
    }
    #endregion


}