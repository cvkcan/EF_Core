using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

#region RelationShips (İlişkiler) Terimleri
#region Principal Entity(Asıl Entity)
//Kendi başına var olabilen tabloyu modelleyen entity'e denir.

//Departmanlar tablosunu modelleyen 'Departman' entity'sidir.
#endregion

#region Dependent Entity(Bağımlı Entity)
//Kendi başına var olamayan, bir başa tabloya bağımlı(ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.

//Calisanlar tablsunu modelleyen 'Calisan' entity'dir.
#endregion

#region Foreign Key
//Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key'dir.

//Dependent Entity'de tanımlanır.

//Principal Entity'deki Principal Key'i tutar.
#endregion

#region Principal Key
//Principal Entity'deki Id'nin kendisidir.

//Principal Entity'nin kimliği olan kolonu ifade eden propertydir.
#endregion


#endregion
class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }
    public Departman Departman { get; set; } //Navigation Property
}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; } //Navigation Property
}
#region Navigation Property Nedir?
//Navigation Property'ler entity'lerdeki tanımlarına göre N'e N yahut 1'e N şeklinde ilişki türlerini ifade etmektedirler. Sonraki derslerimizde ilişkisel yapuları tam teferruatlı pratikte incelerken navigation property'lerin bu özelliklerinden istifade ettiğimizi göreceksiniz.

//İlişkisel tablolar arasındaki fiziksel erişimi entity class'ları üzerinden sağlayan property'lerdir.

//Bir property'nin navigation property olabilmesi için kesinlikle entity türünden olması gerekiyor.
#endregion

#region İlişki Türleri
#region One To One
//Çalışan ile adresi arasındaki ilişki,
//Karı koca arasındaki ilişki,
#endregion

#region One To Many
//Çalışan ile departman arasındaki ilişki,
//Anne ve çocukları arasındaki ilişki,
#endregion

#region Many To Many
//Çalışanlar ile projeler arasındaki ilişki,
//Kardeşler arasındaki ilişki,
#endregion
#endregion

#region Entity Framework Core'da İlişki Yapılandırma Yöntemleri
#region Default Conventions
//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.

//Navigation property'leri kullanarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region Data Annotations Attributes
//Entity'nin niteliklerine göre ince ayarlar yapmamızı sağlayan attribute'lardır. [Key], [ForeignKey]
#endregion

#region Fluent API

//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı sağlayan yöntemdir.

#region HasOne
//İlgili entity'nin ilişkisel entity'ye birebir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion

#region HasMany
//İlgili entity'nin ilişkisel entity'ye çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion

#region WithOne
//HasOne ya da HasMany'den sonra birebir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#region WithMany
//HasOne ya da HasMany'den sonra bire çok ya da çoka çok olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#endregion

#endregion