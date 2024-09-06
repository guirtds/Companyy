using Company.Data;
using Company.Models;
using Company.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Company.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;
    public EmployeeRepository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> AssociateEmployeeToDepartament(int employeeId, int departamentId)
    {
        var empregado = await _dbContext.Employees.FindAsync(employeeId);
        if (empregado == null)
        {
            return null;
        }

        empregado.DepartamentId = departamentId;
        await _dbContext.SaveChangesAsync();

        return empregado;
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        var result = await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
        return result.Entity; 
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var existingEmpregado = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
        if (existingEmpregado == null) return null; 
        

        existingEmpregado.Name = employee.Name;
        existingEmpregado.LastName = employee.LastName;
        existingEmpregado.Email = employee.Email;
        existingEmpregado.Gender = employee.Gender;
        existingEmpregado.PhotoURL = employee.PhotoURL;
        existingEmpregado.DepartamentId = employee.DepartamentId; 

        await _dbContext.SaveChangesAsync();
        return existingEmpregado;
    }

    public async void DeleteEmployeeById(int id)
    {
        var result = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (result != null)
        {
            _dbContext.Employees.Remove(result);
            await _dbContext.SaveChangesAsync();
        }
    }
}
