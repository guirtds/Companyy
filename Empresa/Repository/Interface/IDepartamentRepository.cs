using Company.Models;

namespace Company.Repository.Interface;

public interface IDepartamentRepository
{
    Task<IEnumerable<Departament>> GetDepartaments(); 
    Task<IEnumerable<Employee>> GetEmployeeByDepartament(int id);
    Task<Departament> GetDepartamentById(int id);
    Task<Departament> AddDepartament(Departament departament); 
    Task<Departament> UpdateDepartament(Departament departament); 
    void DeleteDepartamentById(int id); 
}
