// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net.Mime;
using System.Net.WebSockets;

ETicaretContext context = new ETicaretContext();

#region Change Tracking Neydi?
//Context nesnesi üzerinde gelen tüm nesneler/veriler otomatik olarak bir takip mekanizması tarafından izlenirler. İşte bu takip mekanizmasına Change Tracker denir. Change Tracker ile nesneler üzerindeki değişiklikler/işlemler takip edilerek netice itibariyle bu işlemlerin fıtratına uygun bir SQL sorgucukları generate edilir. İşte bu işleme de Change Tracking denir.
#endregion

#region ChangeTracker Propertysi
//Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği taktirde işlemler gerçekleştirmemizi sağlayan bir propertydir.
//Context sınıfının base class'ı olan DbContext sınıfının bir member'idir.

//var urunler= await context.Urunler.ToListAsync();

//urunler[2].Fiyat = 123;
//context.Urunler.Remove(urunler[3]);
//urunler[4].Fiyat = 2;

//var datas=context.ChangeTracker.Entries();
//await context.SaveChangesAsync();

#region DetectChanges Metodu
//EF Core, context nesnesi tarafından izlenen tüm nesnelerdeki değişikleri Change Tracker sayesinde takip edebilmekte eve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık görüntüleri/(snapshot)'ini oluşturabilir.
//Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF Core tarafından otomatik kontrol edilirler.
//Ancak, yapılan operasyonlarda güncel trakcing verilerinden emin olabilmek için değişiklerin algulanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar EF Core değişiklikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 2);
//urun.Fiyat = 111;
//context.ChangeTracker.DetectChanges();//Bu işlem maliyetlidir. Asenkron çalışmalarda kullanılabilir. SaveChange bu işlemi yapar.
//await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si
//İlgili metotlar(SaveChanges, Entries) tarafından DetectChanges metodunu otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan property'dir.
//SaveChanges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisinde default olarak çağırmaktadır. Bu durumda DetectChanges fonksiyonunun kullanımını irademizle yönetmek ve maliyet/performans optimizasyonu yapmal istediğimiz durumlarda AutoDetectChangesEnabled özelliğini kapatabiliriz.

#endregion

#region Entries Metodu
//Context'teki Entry metodunun koleksiyonel versiyonudur.
//Change Tracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry türünden elde etmemizi sağlar ve belirli işlmeler yapabilmemize olanak tanır.
//Entries metodu, DetectChanges metodunu tetikler. Bu durumda da tıpki SaveChanges'da olduğu gibi bir maliyettir. Buradaki maliyetten kaçınmak için AutoDetectChangesEnabled özelliğini false yapmamız gerekir.

//var urunler=await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(x => x.Id == 1).Fiyat = 999; //Update
//context.Urunler.Remove(urunler.FirstOrDefault(x => x.Id == 2)); //Delete
//urunler.FirstOrDefault(x => x.Id == 3).Fiyat = 222; //Update

//context.ChangeTracker.Entries().ToList().ForEach(x =>
//{
//    if (x.State.Equals(EntityState.Unchanged))
//    {
//        //...
//    }
//    else if (x.State.Equals(EntityState.Deleted))
//    {
//        //...
//    }
//    //...
//});

#endregion

#region AcceptAllChanges Metodu
//SaveChanges() veya SaveChanges(true) tetiklendiğinde EF Core herşeyin yolunda olduğunu varsayarak track ettiği verilerin takibini keser yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum/olası bir hata söz konusu olursa eğer FE Core takip ettiği nesneleri bıracakğı için bir düzeltme mevzu bahis olmayacaktır.

//Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotları girecektir.

//SaveChanges(false), EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde yeniden oynayabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye devam eder. Ta ki AcceptAllChanges metodunu irademizle çağırana kadar.

//SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges metodu ile nesnelerden takibi kesebiliriz.

//var urunler=await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u => u.Id == 2).Fiyat = 22; //Update
//urunler.FirstOrDefault(u => u.Id == 3).Fiyat = 23; //Update
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 4)); //Delete

//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();


#endregion

#region HasChanges Metodu
//Takip edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.
//var result = context.ChangeTracker.HasChanges();
#endregion
#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder.

#region Detached
//Nesnenin change tracler mekanizması tarafından takip edilmediğini ifade eder.
//Urun urun = new Urun();
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "asdasd";
//await context.SaveChangesAsync();
#endregion

#region Added
//Veritabanına eklenecek nesneyi ifade eder. Added henüz veritabanına işlenmeyen veriyi ifade eder. SaveChanges fonksiyonu çağırıldığında insert sorgusu oluşturucağı anlamına gelir.
//Urun urun = new() { Fiyat = 12, UrunAdi = "AAA" };
//Console.WriteLine(context.Entry(urun).State);
//await context.Urunler.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//urun.Fiyat = 122;
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
#endregion

#region Unchanged
//Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.
//var urunler=await context.Urunler.ToListAsync();
//var data = context.ChangeTracker.Entries();
//Console.WriteLine();
#endregion

#region Modified
//Nesne üzerinde değişiklil/güncelleme yapıldığını ifade eder. SaveChanges fonksiyonu çağrıldığında update sorgusu oluşturulacağı anlamına gelir.
//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "Bursa";
//Console.WriteLine(context.Entry(urun).State);
//////await context.SaveChangesAsync();
//await context.SaveChangesAsync(false);
//Console.WriteLine(context.Entry(urun).State);

#endregion

#region Deleted
//Nesnenin silindiğini ifade eder. SaveChanges fonksiyonu çağrıldığında delete sorgusu oluşturucağı anlamına gelir.
//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==2);
//Urun urun = new Urun
//{
//    Id=2
//};
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();
#endregion
#endregion

#region Context Nesnesi Üzerinden Change Tracker
//context.ChangeTracker. ////Çoklu işlem
//context.Entry ////Tekil işlem
#region Entry Metodu
//var urun= await context.Urunler.FirstOrDefaultAsync(u=>u.Id==1);
//urun.Fiyat = 291;
//urun.UrunAdi = "ABC";
#region OriginalValues Property'si
//var Fiyat=context.Entry(urun).OriginalValues.GetValue<float>(nameof(urun.Fiyat));
//var urunAdi=context.Entry(urun).OriginalValues.GetValue<string>(nameof(urun.UrunAdi));
#endregion

#region CurrentValues Property'si
//var Fiyat = context.Entry(urun).CurrentValues.GetValue<float>(nameof(urun.Fiyat));
//var urunAdi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(urun.UrunAdi));

#endregion

#region GetDatabaseValues Metodu
//var _urun=await context.Entry(urun).GetDatabaseValuesAsync();
#endregion
#endregion
#endregion

#region Change Tracker'ın Interceptor Olarak Kullanılması
//Yapılan işlemleri veritabanına kaydetmeden önce araya girilip işlemler yapılmasını sağlar.
//Virtaul func olduğu için override edilir. (Context sınıfında)
#endregion

Console.WriteLine("Hello, World!");

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ETicaret; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }
    #region ChangeTracker Videosu - 19
    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    var entries = ChangeTracker.Entries();
    //    foreach (var entry in entries)
    //    {
    //        if (entry.State==EntityState.Added)
    //        {
    //            //...
    //        }
    //        //...

    //        //Gibi base'e gitmeden işlemler döndürebiliriz. Veritabanına gitmeden işlemler döner.
    //    }
    //    return base.SaveChangesAsync(cancellationToken);
    //}
    #endregion

}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}
