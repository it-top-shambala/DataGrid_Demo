using DataGrid_Demo.DB.Models;
using DbConfigLib;
using Microsoft.EntityFrameworkCore;

namespace DataGrid_Demo.DB.DataBase;

public class Db : DbContext
{
    public DbSet<Product> Products { get; set; }

    public Db()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var str = DbConfig.ImportFromJson("db.json").ToString();
        optionsBuilder.UseMySql(str, new MySqlServerVersion(new Version(8, 0, 30)));
    }

    public void CreateDb()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}