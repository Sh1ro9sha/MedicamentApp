using System.Data;
using MedicamentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicamentApp.DataContext
{
    public class MedicamentAppContext : DbContext
    {
        public DbSet<Clients> Clients { get; set; } = null!;
        public DbSet<Pharmacists> Pharmacists { get; set; } = null!;
        public DbSet<Employees> Employees { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<Recipes> Recipes { get; set; } = null!;
        public DbSet<Expenses> Expenses { get; set; } = null!;
        public DbSet<Profit> Profit { get; set; } = null!;
        public DbSet<Drug> Drug { get; set; } = null!;
        public DbSet<Manufacturers> Manufacturers { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Role> Role { get; set; } = null!;

        public MedicamentAppContext(DbContextOptions<MedicamentAppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}