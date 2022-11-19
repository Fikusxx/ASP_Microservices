using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Shop.Services.Identity;


public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext db;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        this.db = db;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public void Initialize()
    {
        if (roleManager.FindByNameAsync(StaticDetails.Admin).GetAwaiter().GetResult() == null)
        {
            roleManager.CreateAsync(new IdentityRole() { Name = StaticDetails.Admin }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = StaticDetails.Customer }).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        var adminUser = new ApplicationUser()
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true,
            PhoneNumber = "123",
            FirstName = "Admin",
            LastName = "Admin"
        };

        userManager.CreateAsync(adminUser, "Abcd123!").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(adminUser, StaticDetails.Admin).GetAwaiter().GetResult();
        var temp1 = userManager.AddClaimsAsync(adminUser, new List<Claim>()
        { 
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, StaticDetails.Admin),
        }).Result;

        var customerUser = new ApplicationUser()
        {
            UserName = "customer@customer.com",
            Email = "customer@customer.com",
            EmailConfirmed = true,
            PhoneNumber = "123",
            FirstName = "customer",
            LastName = "customer"
        };

        userManager.CreateAsync(customerUser, "Abcd123!").GetAwaiter().GetResult();
        userManager.AddToRoleAsync(customerUser, StaticDetails.Customer).GetAwaiter().GetResult();
        var temp2 = userManager.AddClaimsAsync(customerUser, new List<Claim>()
        {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, StaticDetails.Customer),
        }).Result;

    }
}

