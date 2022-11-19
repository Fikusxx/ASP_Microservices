using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Services.Identity;


var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;


string connection = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                                                     .AddDefaultTokenProviders();

var identityBuilder = services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddInMemoryIdentityResources(StaticDetails.IdentityResources)
    .AddInMemoryApiScopes(StaticDetails.ApiScopes)
    .AddInMemoryClients(StaticDetails.Clients)
    .AddAspNetIdentity<ApplicationUser>();

services.AddScoped<IDbInitializer, DbInitializer>();

identityBuilder.AddDeveloperSigningCredential();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
