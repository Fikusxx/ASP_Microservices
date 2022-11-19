using Shop.Web;



var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;


services.AddHttpClient<IProductService, ProductService>();
services.AddHttpClient<ICartService, CartService>();
services.AddHttpClient<ICouponService, CouponService>();

StaticDetails.ProductAPIBase = configuration["ServiceUrls:ProductAPI"];
StaticDetails.ShoppingCartAPIBase = configuration["ServiceUrls:ShoppingCartAPI"];
StaticDetails.CouponAPIBase = configuration["ServiceUrls:CouponAPI"];

services.AddScoped<IProductService, ProductService>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<ICouponService, CouponService>();

services.AddControllersWithViews();

services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", x => x.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = configuration["ServiceUrls:IdentityAPI"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "shop";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("shop");
        options.SaveTokens = true;
    });



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
