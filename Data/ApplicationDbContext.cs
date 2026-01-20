using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechNovaLeave.Models;

namespace TechNovaLeave.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<LeaveModel> Leaves { get; set; }
    }
}
