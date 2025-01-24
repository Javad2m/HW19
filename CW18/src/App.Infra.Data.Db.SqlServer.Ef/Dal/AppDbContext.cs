using App.Domain.Core.hw15.Card.Entity;
using App.Domain.Core.hw15.Transaction.Entity;
using App.Domain.Core.hw15.User.Entity;
using App.Infra.Data.Db.SqlServer.Ef.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Db.SqlServer.Ef.Dal;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Data Source=WKK13\\SQLEXPRESS;Initial Catalog=hw18;User ID=sa;Password=111222333aA;TrustServerCertificate=True;Encrypt=True");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
}
    