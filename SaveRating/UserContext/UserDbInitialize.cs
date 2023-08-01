namespace WebApplication1.UserContext;

public abstract class UserDbInitialize
{
    public static void Initialize(IServiceProvider servicesProvider)
    {
        var context = servicesProvider.GetRequiredService<UserDbContext>();
        context.Database.EnsureCreated();
    }
}