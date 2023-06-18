using lesson13.Migrations;
using Microsoft.EntityFrameworkCore;
//<<<<<<< HEAD
using System.Collections.Immutable;
//=======
//>>>>>>> ed75b10a2ece2bef5a09257ea4fb9550363f9f60
using System.Net.Mime;
using System.Threading.Channels;

Console.WriteLine("Hello, World!");

ETicaretContext context = new ETicaretContext();

#region En Temel Basit Bir Sorgulama Nasıl Yapılır?

#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//              select urun).ToListAsync();
//Buradaki yapılanma linq
#endregion

#endregion
#region IQueryable ve IEnumerable Nedir? Basit Olarak!

//var urunler = from urun in context.Urunler
//              select urun;
//Yukarıdaki IQueryable'dır.

//var urunler = await (from urun in context.Urunler
//                     select urun).ToListAsync();
//Yukaridaki IEnumerable'dır.

#region IQueryable
//Sorguya karşılık gelir.
//EF Core üzerinde yapılmış olan sorgunun execute edilmemiş halini ifade eder.
#endregion
#region IEnumerable
//Sorgunun çalıştırılıp/execute edilip verilerin in memorye yüklenmiş halini ifade eder.
#endregion
#endregion
#region Sorguyu Execute Etmek İçin Ne Yapmamız Gerekmektedir?
#region ToListAsync
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//                     select urun).ToListAsync();
#endregion

#endregion
//int urunId = 2;
//string urunAdi = "B";
//var urunler = from urun in context.Urunler
//              where urun.Id == urunId && urun.UrunAdi.Contains(urunAdi)
//              select urun;
//urunId = 3;
//urunAdi="C";
////foreach (var urun in urunler)
////{
////    Console.WriteLine(urun.UrunAdi);
////}

//await urunler.ToListAsync();

#region Foreach
//var urunler = from urun in context.Urunler
//              select urun;

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.Id);
//}
#endregion
#region Deferred Execution (Ertelenmiş Çalışma)
//IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz yani ilgili kod yazıldığı noktada sorguyu generate etmez! Nerede eder? Çalıştırıldığı/execute edildiği noktada tetkilenir! İşte bu durumda ertelenmiş çalışma denir!

#endregion
#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları
#region ToListAsync
//Üretilen sorguyu execute ettirmemizi sağlayan fonksiyondur.
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              select urun;
//var datas = await urunler.ToListAsync();

//var urunler = await (from urun in context.Urunler
//                     select urun).ToListAsync();
#endregion
#endregion
#region Where
//Oluşturulan sorguya where şartı eklememizi sağlayan bir fonskiyondur.
#region Method Syntax
//var urunler = await context.Urunler.Where(x => x.Id == 2).ToListAsync();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              where urun.Id == 2
//              select urun;
//var datas=await urunler.ToListAsync();

//var urunler = await (from urun in context.Urunler
//                     where urun.Id == 2 || urun.UrunAdi.StartsWith("A")
//                     select urun).ToListAsync();
#endregion
#endregion
#region OrderBy
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur. Default olarak Ascending.
#region Method Syntax
//var urunler= await context.Urunler.Where(u=>u.Id>=2).OrderBy(u=>u.Fiyat).ToListAsync();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              where urun.Id >= 2
//              orderby urun.Fiyat /* ascending */
//              select urun;
//var datas=await urunler.ToListAsync();
#endregion
#endregion
#region ThenBy
//OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamanızı sağlayan bir fonksiyondur. (Ascending)
//OrderBy üzerinde yapılan sıralamlarda tekrar eden varsa diğer kolonlara göre de sıralama yaptırılır.
#region Method Syntax
//var urunler = await context.Urunler.Where(u => u.Id >= 2).OrderBy(u => u.Fiyat).ThenBy(u => u.UrunAdi).ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//                      where urun.Id>=2
//                      orderby urun.Fiyat , urun.UrunAdi
//                      select urun).ToListAsync();
#endregion
#endregion
#region OrderByDescending
//Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur.
#region Method Syntax
//var urunler = await context.Urunler.Where(x => x.Id >= 2).OrderByDescending(x => x.UrunAdi).ToListAsync();
#endregion
#region Query Syntax
//var urunler=await (from c in context.Urunler
//             where c.Id>=2
//             orderby c.Fiyat descending
//             select c).ToListAsync();
#endregion
#endregion
#region ThenByDescending
//Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur.
#region Method Syntax
//var urunler=await context.Urunler.Where(u=>u.Id>=2).OrderBy(u=>u.Fiyat).ThenByDescending(u=>u.UrunAdi).ToListAsync();
#endregion
#region Query Syntax
//var urunler=await (from urun in context.Urunler
//             where urun.Id>=2
//             orderby urun.Fiyat, urun.UrunAdi descending
//             select urun).ToListAsync();
#endregion
#endregion
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan sorguda sadece ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir.
#region SingleAsync
//Eğer ki , sorgu neticesinde birden fazka veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception furlatır.
#region Tek Kayıt Geldiğinde
//Urun urun = await context.Urunler.SingleAsync(u => u.Id == 2);
#endregion
#region Hiç Kayıt Gelmediğinde
//Urun urun = await context.Urunler.SingleAsync(u => u.Id == 22);
#endregion
#region Çok Kayıt Geldiğinde
//var s = await context.Urunler.SingleAsync(u=>u.Id>=1);
#endregion
#endregion
#region SingleOrDefaultAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyorsa null döner.
#region Tek Kayıt Geldiğinde
//Urun urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 2);
#endregion
#region Hiç Kayıt Gelmediğinde
//Urun urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 22);
#endregion
#region Çok Kayıt Geldiğinde
//var s = await context.Urunler.SingleOrDefaultAsync(u => u.Id >= 1);
#endregion
#endregion
//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir.
#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#region Tek Kayıt Geldiğinde
//Urun urun = await context.Urunler.FirstAsync(u => u.Id == 2);
#endregion
#region Hiç Kayıt Gelmediğinde
//Urun urun = await context.Urunler.FirstAsync(u => u.Id >= 10);
#endregion
#region Çok Kayıt Geldiğinde
//var s = await context.Urunler.OrderByDescending(u => u.Fiyat).FirstAsync(u => u.Id >= 1);
#endregion
#endregion
#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değerini döndürür.
#region Tek Kayıt Geldiğinde
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 2);
#endregion
#region Hiç Kayıt Gelmediğinde
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id >= 10);
#endregion
#region Çok Kayıt Geldiğinde
//var s = await context.Urunler.OrderByDescending(u=>u.Fiyat).FirstOrDefaultAsync(u => u.Id >= 1);
#endregion
#endregion
#region SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırma

#endregion
#region FindAsync
//Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.
#region Component Primary Key Durumu
//Urun urun = await context.Urunler.FindAsync(2);
#endregion
//Urun urun = await context.Urunler.FindAsync(1,2);
#endregion
#region FindAsync İle SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Karşılaştırma

#endregion
#region LastAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır. OrderBy kullanılması zorunludur.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u=>u.Id==1);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u=>u.Id==99);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u=>u.Id>=1);
#endregion

#endregion
#region LastOrDefaultAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. Eğer ki hiç veri gelmiyorsa null döner. OrderBy kullanılması zorunludur.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id == 1);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id == 99);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id >= 1);
#endregion
#endregion
#endregion

//<<<<<<< HEAD
#region Diğer Sorgulama Fonksiyonları
#region CountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (int) bizlere bildiren fonksiyondur.
//var urunler=(await context.Urunler.ToListAsync()).Count();
//Yukarıdaki sorgu neticesinde ürünler in memory'e geldikten sonra sayılır. Maliyetlidir ve yapılmaz.
//var urunler = await context.Urunler.CountAsync();
#endregion
#region LongCountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (long) bizlere bildiren fonksiyondur.
//var urunler = await context.Urunler.LongCountAsync();
#endregion
#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur.
//var uruns = await context.Urunler.AnyAsync(u=>u.Id>=2);
//var urunler = await context.Urunler.AnyAsync(x=>x.Id>99);
#endregion
#region MaxAsync
//Verilen kolondaki max değeri döner.
//var u = await context.Urunler.MaxAsync(x=>x.Id);
//var us = await context.Urunler.MaxAsync(x=>x.UrunAdi);
#endregion
#region MinAsync
//Verilen kolondaki min değeri döner.
//var urunler = await context.Urunler.MinAsync(x=>x.Fiyat);
#endregion
#region Distinct
//Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
//var urunler = await context.Urunler.Distinct().ToListAsync();
//Distinc dönüş türü IQueryable olduğu için ToList kullanılır. Geri dönüş tipine bak.
#endregion
#region AllAsync
//Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true, uymuyorsa false döndürecektir.
//var uuu = await context.Urunler.AllAsync(x=>x.Id>=99);
//var urunler = await context.Urunler.AllAsync(u => u.UrunAdi.Contains("a"));
#endregion
#region SumAsync
//Vermiş olduğumuz sayısal property'in toplamını alır.
//var FiyatToplam = await context.Urunler.SumAsync(x=>x.Id);
#endregion
#region AverageAsync
//Vermiş olduğumuz sayısal property'in ortalamasını alır.
//var AritmatikFiyat = await context.Urunler.AverageAsync(x => x.Fiyat);
#endregion
#region ContainsAsync
//Like '%...%' sorgusu oluşturmamızı sağlar.
//var urunler = await context.Urunler.Where(x => x.UrunAdi.Contains("a")).ToListAsync();
#endregion
#region StartsWith
//Like '%...' sorugusu oluşturmamızı sağlar.
//var urunle = await context.Urunler.Where(x=>x.UrunAdi.StartsWith("a")).ToListAsync();
#endregion
#region EndsWith
//Like '...%' sorgusu oluşturmamızı sağlar.
//var urunle = await context.Urunler.Where(x => x.UrunAdi.EndsWith("a")).ToListAsync();
#endregion
#endregion

//=======
//>>>>>>> ed75b10a2ece2bef5a09257ea4fb9550363f9f60

#region Sorgu Sonucu Dönüşüm Fonksiyonları
//Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri istediğimiz doğrultusunda farklı türlerde projection edebiliyoruz.
#region ToDictionaryAsync
//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılamak istiyorsak eğer kullanılır!
//var urunler = await context.Urunler.ToDictionaryAsync(u => u.Id);
//ToList ile aynı amaca hizmet etmektedir. Yani, oluşturulan sorguyu execute edip neticesini alırlar.
//ToList : Gelen sorgu neticesini entitiy türünde bir colection (List<TEntity>) dönüştürmekteyken, ToDictionary : ise gelen sorgu neticesini Dictioanry türünden bir koleksiyona dönüştürecektir.
#endregion
#region ToArrayAsync
//Oluşturulan sorguyu dizi olarak elde eder.
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gtelen sonucu entity dizisi olarak elde eder.
//var urunler = await context.Urunler.ToArrayAsync();
#endregion
#region Select
//Sekect fonksiyonu işlevsel olarak birden fazla davranışı söz konusudur.
//1) Select fonksiyonu, generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır.
//var uruns = await context.Urunler.Select(u=> new Urun
//{
//    Id = u.Id,
//    UrunAdi=u.UrunAdi
//}).ToListAsync();

//2) Select fonksiyonu, gelen verileri farklı türlerden karşılamamızı sağlar. T, anonymous type.
//var urunls =await context.Urunler.Select(u=>new { 
//    Id=u.Id,
//    UrunAdi=u.UrunAdi
//}).ToListAsync();
//Yukarıdaki kod anonymous type.

//var urunler = await context.Urunler.Select(u=> new UrunDetay { 
//    Fiyat = u.Fiyat,
//    Id=u.Id
//}).ToListAsync();
//Yukarıdaki ise T.
#endregion
#region SelectMany
//var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u=>u.Parcalar, (u,p) => new
//{
//    u.Id,
//    u.Fiyat,
//    p.ParcaAdi
//}).ToListAsync();
#endregion
#endregion

#region GroupBy Fonksiyonu
//Gruplama yapmamızı sağlar.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(u=>u.Fiyat).Select(x=>new
//{
//    Count=x.Count(),
//    Fiyat=x.Key
//}).ToListAsync();
#endregion
#region Query Syntax
//var datas = await (from urun in context.Urunler
//            group urun by urun.Fiyat
//           into @group
//            select new
//            {
//                Count=@group.Count(),
//                Fiyat=@group.Key
//            }).ToListAsync();
#endregion
#endregion
#region Foreach Fonksiyonu
//Bir sorgulama fonksiyonu felan değildir.
//Sorgualam neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemiiz ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. Foreach döngüsünün metot halidir.

//foreach (var item in datas)
//{
//    item.Count = 0; // Anonymous type'lar sadece readonly'dir.!
//    Console.WriteLine(item.Count + " " + item.Fiyat);
//}

//datas.ForEach(x =>
//{
//    Console.WriteLine($"{x.Count} {x.Fiyat}");
//});

#endregion

Console.WriteLine();

public class ETicaretContext:DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ETicaret; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
    }
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

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }
}
