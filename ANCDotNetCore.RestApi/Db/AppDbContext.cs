﻿using ANCDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ANCDotNetCore.RestApi.Db;

internal class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}
