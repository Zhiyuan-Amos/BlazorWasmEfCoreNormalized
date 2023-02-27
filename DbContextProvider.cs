using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace BlazorWasmEfCoreNormalized;

public class DbContextProvider
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private readonly Task _firstTimeSetupTask;

    public DbContextProvider(IJSRuntime js, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _firstTimeSetupTask = FirstTimeSetupAsync();
        
        async Task FirstTimeSetupAsync()
        {
            await js.InvokeVoidAsync("synchronizeFileWithIndexedDb", Constants.DatabaseFileName);
            await using var db = await _dbContextFactory.CreateDbContextAsync();
            await db.Database.EnsureCreatedAsync();
        }
    }

    public async Task<AppDbContext> GetPreparedDbContextAsync()
    {
        await _firstTimeSetupTask;
        return await _dbContextFactory.CreateDbContextAsync();
    }
}