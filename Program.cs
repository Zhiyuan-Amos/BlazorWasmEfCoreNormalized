using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasmEfCoreNormalized;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddDbContextFactory<AppDbContext>(options => options.UseSqlite($"Filename={Constants.DatabaseFileName}"))
    .AddScoped<DbContextProvider>();

await builder.Build().RunAsync();