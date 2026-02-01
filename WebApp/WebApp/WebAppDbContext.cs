using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DataAccess;
public class WebAppDbContext : DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }

    #region models

    public DbSet<UUser> UUsers { get; set; } = null!;

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 継承元のFluentAPIを実行
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (clrType != null && typeof(IdAutoBase).IsAssignableFrom(clrType))
            {
                var configType = typeof(IdAutoBaseConfiguration<>).MakeGenericType(clrType);
                var configInstance = Activator.CreateInstance(configType);

                var applyConfigMethod = typeof(ModelBuilder)
                    .GetMethods()
                    .First(m => m.Name == "ApplyConfiguration" && m.GetParameters().Length == 1)
                    .MakeGenericMethod(clrType);

                applyConfigMethod.Invoke(modelBuilder, new[] { configInstance });
            }
        }

        // 外部キー制約の設定
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            // 参照されているカラムを削除しようとするとエラーになる
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }

        // FluentAPIが存在する場合は実行
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebAppDbContext).Assembly);
    }
}