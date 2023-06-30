using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Threading.Channels;

ETicaretContext context = new ETicaretContext();

#region AsNoTracking Metodu
//Context üzerinden gelen tüm datalar Change Tracker mekanizması tarafından takip edilmektedir.

//Change Tracker, takip ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde bir maliyete sahiptir. O yüzden üzerinde işlem yapılmayacak verilerin takip edilmesi bizlere lüzumsuz yere bir maliyet ortaya çıkaracaktır.

//AsNoTracking metodu ile Change Tracker'ın ihtiyaç olöayam verilerdeki maliyeti törpülemiş oluruz.

//AsNoTracking fonksiyonu ile yapılan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanabilir lakin veriler üzerinde herhangi bir değişiklik/update işlemi yapamayız.

//var kullanicilar=await context.Kullanicilar.AsNoTracking().ToListAsync(); //IQueryable iken AsNoTracking çağırılır.
//foreach (var kullanici in kullanicilar)
//{
//    Console.WriteLine(kullanici.Adi);
//    kullanici.Adi = $"yeni-{kullanici.Adi}";
//    context.Kullanicilar.Remove(kullanici); //Bu şekide silme
//    context.Kullanicilar.Update(kullanici); //Bu şekilde güncelleme
//}
//await context.SaveChangesAsync();
#endregion

//Change Tracker mekanizması çalıştığı sürece ilişkisel verilerde yineleme yapılmaz fakat maliyetli olacağından AsNoTrackingWithIdentityResolution kullanılır.

#region AsNoTrackingWithIdentityResolution
//CT (Change Tracker) mekanizması yinelenen verileri tekil instance olarak getirir. Buradan ekstradan bir performans kazancı söz konusudur.

//Bizler yaptığımız sorgularda takip mekanizmasının AsNoTracking metodu ile maliyetini kırmak isterken bazen maliyete sebebiyet verebiliriz. (Özellikle ilişkisel tabloları sorgularken bu duruma dikkat etmemiz gerekiyor)

//AsNoTracking ile elde edilen veriler takip edilemeyeceğinden dolayı yinelenen verilerin ayrı instanclerda olmasına sebebiyet veriyoruz. Çünkü CT mekanizması takip ettiği nesneye ayrı noktalardaki ihtiuacı aynı instance üzerinden gidermektedir.

//Böyle bir durumda hem takip mekanizmasının maliyetini ortadan kaldırmak hem de yinelenen dataları tek bir instance üzerinden karşılamak için AsNoTrackingWithIdentityResolution fonksiyonu kullanabiliriz.

//var kitaplar = await context.Kitaplar.Include(x => x.Yazarlar).AsNoTrackingWithIdentityResolution().ToListAsync();

//AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna nazaran görece yavaştır/maliyetlidir fakat CT'ye nazaran daha permanslı ve az maliyetlidir.
#endregion

#region AsTracking
//Context üzerinden gelen dataların CT tarafından takip edilmesini iradeli bir şekilde ifade etmemizi sağlayan fonksiyonudur.

//Bir sonraki inceleyeceğimiz UseQueryTrackingBehavior metodunun davranışı gereği uygulama seviyesinde CT'nin default olarak devrede olup olmamasını ayarlıyor olacağız. Eğer ki default olarak pasif hale getirilirse böyle durumda takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking fonksiyonunu kullanabiliri ve böylece takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking fonksiyonunu kullanabilir ve böylece takip mekanizmasını iradeli bir şekkilde devreye sokmuş oluruz.

//var kitaplar = await context.Kitaplar.AsTracking().ToListAsync();
#endregion

#region UseQueryTrackingBehavior
//EF Core seviyesinde/uygulama seviyesinde ilgili contexten gelen verilerin üzerinde CT mekanizmasının davranışı temel seviyede belirlememizi sağlayan fonksiyondur. Yani configurasion fonksiyonudur.
#endregion

public class ETicaretContext:DbContext
{
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Rol> Roller { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseSqlServer("");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}

public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Rol> Roller { get; set; }
}

public class Rol
{

}

public class Kitap
{
    public Kitap()
    {
        Console.WriteLine("Kitap");
    }
    public string KitapAdi { get; set; }
    public int SayfaSayisi { get; set; }
    public int Id { get; set; }
    public ICollection<Yazar> Yazarlar { get; set; }
}

public class Yazar
{
    public Yazar() => Console.WriteLine("Yazar");
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<Kitap> Kitaplar { get; set; }
}