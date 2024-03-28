using Bacola.Core.Entities;
using Bacola.Data;
using Bacola.Data.Contexts;
using Bacola.Data.RepositoryRegisterations;
using Bacola.Service.Mappers;

using Bacola.Service.ServiceRegisterations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(option=>option.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(typeof(GlobalMapping));
builder.Services.AddDbContext<BacolaDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.DataAccessServiceRegister(builder.Configuration);
builder.Services.ServiceLaierServiceRegister();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();


app.UseEndpoints(endpoint =>
{

    endpoint.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
    );
    endpoint.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

var scopFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(roleManager, userManager);
}


app.Run();

