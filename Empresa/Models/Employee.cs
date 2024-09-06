using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models;

[Table("Employees_PX")]
public class Employee
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName {  get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set; }
    public int DepartamentId { get; set; } // Chave estrangeira para o departamento
    public string? PhotoURL { get; set; }
}
