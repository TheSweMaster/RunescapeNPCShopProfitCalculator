using Microsoft.EntityFrameworkCore;
using RunescapeNPCShopCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunescapeNPCShopCalculator.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ShopDetail> ShopDetails { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }

    }
}
