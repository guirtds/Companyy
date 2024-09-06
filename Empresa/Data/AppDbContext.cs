using Company.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Departament> Departaments { get; set;}

}
