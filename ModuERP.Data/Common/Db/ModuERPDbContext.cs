using Microsoft.EntityFrameworkCore;
using ModuERP.Data.Common.Entities;
using ModuERP.Data.Common.Entities.Inventory;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ModuERP.Data.Common.Db;

public class ModuERPDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    public ModuERPDbContext(DbContextOptions<ModuERPDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuERPDbContext).Assembly);
    }
}
