using Company.Models;

namespace Company.Repository.Interface;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees(); 
    Task<Employee> GetEmployeeById(int id); 
    Task<Employee> AddEmployee(Employee employee); 
    Task<Employee> UpdateEmployee(Employee employee); 
    void DeleteEmployeeById(int id);
}
