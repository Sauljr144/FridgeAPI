using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Startup
{
public void ConfigureServices(IServiceCollection services)
{
    // Add JWT authentication
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key")),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true
            };
        });

    // Register JwtService with a scoped lifetime
    services.AddScoped<JwtService>();
}
}