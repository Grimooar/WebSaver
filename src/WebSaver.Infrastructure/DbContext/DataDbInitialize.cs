using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DbContext;

public class DataDbInitializer {
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<DataDbContext>();
        context.Database.EnsureCreated();
    }
}