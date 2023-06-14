using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;

Console.WriteLine("Hi");

//EF CORE

//•	DbContext: veritabanını temsil edecek olan sınıftır. Ve DbContext’ten türetilmelidir. Temel yapılandırmalardan sorumludur. Sorgulama operasyonlarını yürütür. SQL dönüşümü ve veritabanına gönderme yapar.
//• DbFirst için kullanılacak komutlar:
//• Scaffold-DbContext 'ConnectionString' Microsoft.EntityFrameworkCore.[provider]
//• Scaffold-DbContext 'ConnectionString' Microsoft.EntityFrameworkCore.[provider] -Tables table1,table2 ...
//• Scaffold-DbContext 'ConnectionString' Microsoft.EntityFrameworkCore.[provider] -Context ContextName
//• Scaffold-DbContext 'ConnectionString' Microsoft.EntityFrameworkCore.[provider] -ContextDir Data -OutputDir Models.
//•	Entity: tablolarımı temsil edecek sınıf. Tablolar çoğul iken Entity’ler tekil olur.
//•	Entityler’in DbSet’teki adları çoğuldur. Tablolara denktir. 
//•	DBFirst’te önce veritabanı oluşturulur. Sonrasında scaffold ile gerçekleştiriz.
//•	CodeFirst: Code kısmındakini aktarmak için Migration şart.
//•	Migration: Code kısmında oluşturduklarımızı database’nin anlayacağı hale getiren bir c# class’tır.
//•	OnConfiguring: Temel ayarlama yapmamızı sağlar.
//•	Migration sonucunda Migrations klasörü içerisinde ismini verdiğimiz migration içerisinde Up ve Down vardır. Tabloyu Up oluşturmayı sağlarken Down ise silmeyi sağlar.
//•	Migration oluşturduktan sonra update-database ile migrate ediyoruz. Birden fazla migration varsa Db içerisinde History diye table oluşturulur. İstenilen seviyede database’yi çalıştırabiliriz.
//•	Kod üzerinde de migrate operasyonunu yapabiliyoruz. Bunun için de DbContext’ten nesne oluşturup await context.Database.MigrateAsync(); ile uygulama ayağa kalktığı zaman database’yi oluşturabiliriz.
//•	Yapılan değişiklikler kesinlikle manuel olarak veritabanında yapılmaz. Önce kod kısmında yapılıp sonrasında migration edilir. Sonrasında da migrate edilerek veritabanına yansıtılır.
//•	Migration için komutlar:
//add - migration[migration name]
//add - migration[migration name] - OutputDir[path]
//remove - migration
//update - database => up etme
//update-database  [migration name] => down etme
