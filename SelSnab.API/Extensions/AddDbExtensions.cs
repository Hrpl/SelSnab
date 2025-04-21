
using SelSnab.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EMDR42.API.Extensions;

public static class AddDbExtensions
{
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
            $"Host=89.169.2.95;Port=5432;Database=SelSnab;Username=postgres;Password=2208;"
        ));
        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
