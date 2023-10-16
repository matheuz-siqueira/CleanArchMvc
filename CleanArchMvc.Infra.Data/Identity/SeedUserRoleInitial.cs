using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager; 
        _roleManager = roleManager; 
    }
    public void SeedUsers()
    {
        if(_userManager.FindByEmailAsync("user@localhost").Result is null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "user@localhost";
            user.Email = "user@localhost";
            user.NormalizedEmail = "USER@LOCALHOST";
            user.NormalizedUserName = "USER@LOCALHOST";
            user.EmailConfirmed = true; 
            user.LockoutEnabled = false; 
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, "P4ssword@").Result;
            if(result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if(_userManager.FindByEmailAsync("admin@localhost").Result is null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "admin@localhost";
            user.Email = "admin@localhost";
            user.NormalizedEmail = "ADMIN@LOCALHOST";
            user.NormalizedUserName = "ADMIN@LOCALHOST";
            user.EmailConfirmed = true; 
            user.LockoutEnabled = false; 
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, "P4ssword@").Result;
            if(result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
    public void SeedRoles()
    {
        if(!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "User";
            role.NormalizedName = "USER";
            IdentityResult result = _roleManager.CreateAsync(role).Result; 
        }
        if(!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            IdentityResult result = _roleManager.CreateAsync(role).Result; 
        }
    }
}
