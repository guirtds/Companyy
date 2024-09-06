using Company.Data;
using Company.Models;
using Company.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Company.Repository;

public class DepartamentRepository : IDepartamentRepository
{
    private readonly AppDbContext _dbContext;

    public DepartamentRepository(AppDbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }

    public async Task<IEnumerable<Departament>> GetDepartaments()
    {
        return await _dbContext.Departaments.ToListAsync();
    }

    public async Task<Departament> GetDepartamentById(int id)
    {
        return await _dbContext.Departaments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetEmployeeByDepartament(int id)
    {
        var departamento = await _dbContext.Departaments.FindAsync(id);

        if (departamento == null)
        {
            return Enumerable.Empty<Employee>(); // Retorna uma lista vazia se o departament não existir
        }

        return await _dbContext.Employees
            .Where(e => e.DepartamentId == id)
            .ToListAsync();
    }

    public async Task<Departament> AddDepartament(Departament departament)
    {
        var result = await _dbContext.Departaments.AddAsync(departament);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Departament> UpdateDepartament(Departament departament)
    {
        var existingDepartamento = await _dbContext.Departaments.FirstOrDefaultAsync(d => d.Id == departament.Id);
        if (existingDepartamento == null)
        {
            return null; // Retorna null se o departament não for encontrado
        }

        // Atualiza o nome do departament
        existingDepartamento.Name = departament.Name;

        await _dbContext.SaveChangesAsync();
        return existingDepartamento;
    }

    public async void DeleteDepartamentById(int id)
    {
        var result = await _dbContext.Departaments.FirstOrDefaultAsync(d => d.Id == id);
        if (result != null)
        {
            _dbContext.Departaments.Remove(result);
            await _dbContext.SaveChangesAsync();
        }
    }
}
