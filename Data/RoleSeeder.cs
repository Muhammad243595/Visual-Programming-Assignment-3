using Microsoft.AspNetCore.Identity;

public static class RoleSeeder
{
    public static async Task SeedRoles(IServiceProvider service)
    {
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Employee"))
            await roleManager.CreateAsync(new IdentityRole("Employee"));
    }
}
