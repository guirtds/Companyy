using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models;

[Table("Departaments_PX")]
public class Departament
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
