using Microsoft.EntityFrameworkCore;
using ANCDotNetCore.NLayer.DataAccess.Models;

namespace ANCDotNetCore.NLayer.DataAccess.Db;

internal class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}
