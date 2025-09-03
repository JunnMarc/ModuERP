using Microsoft.EntityFrameworkCore;
using ModuERP.Data.Common.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ModuERP.Data.Common.Db;

public class ModuERPDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public ModuERPDbContext(DbContextOptions<ModuERPDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuERPDbContext).Assembly);
    }
}
