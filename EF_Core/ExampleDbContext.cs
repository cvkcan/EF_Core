using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core
{
    internal class ExampleDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=CAN\SQLEXPRESS; Database=ExampleDb; User Id=sa; Password=Windowserver19*; TrustServerCertificate=True");
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Factory { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }

    }

    public class Customer
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Assa { get; set; }
        public int MyProperty { get; set; }
    }
}
