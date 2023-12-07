using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeAPI.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<FridgeItemModel> FridgeItemInfo { get; set; }
        public DbSet<ShoppingItemModel> ShoppingItemInfo { get; set; }
      

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

         //function to create the tables themselves
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
    }
}