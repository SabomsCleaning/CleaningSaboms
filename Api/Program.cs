using CleaningSaboms.Models;
using CleaningSaboms.Seed;
using CleaningSaboms.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CleaningSaboms.Context.DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure())
    );
builder.Services.AddServices();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//{
//    options.Password.RequiredLength = 6;
//    options.Password.RequireDigit = true;
//    options.User.RequireUniqueEmail = true;
//})
//.AddEntityFrameworkStores<CleaningSaboms.Context.DataContext>()
//.AddDefaultTokenProviders();

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
//    await RoleSeed.SeedRolesAsync(roleManager);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
