using Microsoft.EntityFrameworkCore;
using DepositParameters.Models;

namespace DepositParameters;

public class AppDbContext : DbContext
{
    /// <summary>
    /// Месторождения
    /// </summary>
    public DbSet<Deposit> Deposits { get; set; }

    /// <summary>
    /// Замеры
    /// </summary>
    public DbSet<Data> Data { get; set; }

    /// <summary>
    /// Причины отклонений
    /// </summary>
    public DbSet<Reason> Reasons { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Data.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //заполняем базу данными
        modelBuilder.Entity<Deposit>()
            .HasData(new Deposit[]
            {
                new Deposit { Id = 1, Name = "41 / Ичединское" }
            });

        modelBuilder.Entity<Data>()
            .HasData(new Data()
            {
                Id = 1,
                DepositId = 1,
                Status = Status.Approved,
                Date = new DateTime(2020, 8, 8)
            });

        modelBuilder.Entity<Data>()
            .OwnsOne(x => x.Qj)
            .HasData(new
            {
                DataId = 1,
                Value = (decimal)30,
                Status = Status.None
            });

        modelBuilder.Entity<Data>()
            .OwnsOne(x => x.Wp)
            .HasData(new
            {
                DataId = 1,
                Value = (decimal)61,
                Status = Status.None
            });

        modelBuilder.Entity<Data>()
            .OwnsOne(x => x.Qn)
            .HasData(new
            {
                DataId = 1,
                Value = (decimal)24.57,
                Status = Status.None
            });


        modelBuilder.Entity<Reason>()
            .HasData(new Reason[]
            {
                new Reason { Id = 1, Name = "Изменение коэффициента продуктивности" }
            });
    }
}
