using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBook1
{
    public class OrderBookContext : DbContext
    {
        //DbSets
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        public string DbPath { get; private set; }

        //Constructor
        public OrderBookContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}orders.db";
        }

            // The following configures EF to create a Sqlite database file in the
            // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    
    }
}
