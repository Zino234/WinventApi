using Microsoft.EntityFrameworkCore;
using Winvent.Application.Interface;
using Winvent.Application.Repositries;
using Winvent.Infrastructure.Data;
using Winvent.Infrastructure.Respositries;
using Winvent.Application.Services;
using Winvent.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


//var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
//var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddScoped<IAdminRepository, AdminRepositries>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IOfficerRepository, OfficerRepositories>();
builder.Services.AddScoped<IOfficerService, OfficerService>();

builder.Services.AddScoped<IOfferingRepository, OfferingRepositories>();
builder.Services.AddScoped<IOfferingService, OfferingService>();

builder.Services.AddScoped<IExpenseRespository, ExpenseRepositories>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<ITitheRepository, TitheRepositories>();
builder.Services.AddScoped<ITitheService, TitheService>();

builder.Services.AddScoped<ITransportSeedRepository, TransportSeedRepositories>();
builder.Services.AddScoped<ITransportSeedService, TransportSeedService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
       );
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
    );


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
}
);

//Registering the build pipeline
builder.Services.AddDbContext<WinventDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WinventApi")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
