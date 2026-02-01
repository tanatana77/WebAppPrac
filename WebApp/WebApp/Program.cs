using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

if (builder.Environment.IsProduction())
{
    try
    {
        var keyVaultUrl = new Uri("https://prac-dev-key.vault.azure.net/");

        builder.Configuration.AddAzureKeyVault(
            keyVaultUrl,
            new DefaultAzureCredential());
    }
    catch (Exception ex) {
        // エラーログを握りつぶす
        Console.WriteLine("Key Vaultが読み込まれていないです。\n");
        Console.WriteLine(ex.ToString());
    }
}

builder.Services.AddDbContext<WebAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    if (builder.Environment.IsDevelopment())
    {
        // EF Coreのクエリログのパラメータマスク無効化
        options.EnableSensitiveDataLogging();
    }
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
