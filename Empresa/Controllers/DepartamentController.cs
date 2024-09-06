using Company.Models;
using Company.Repository;
using Company.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers;

[Route("api/departament")]
[ApiController]
public class DepartamentController : ControllerBase
{
    private readonly IDepartamentRepository _departamentRepository;

    public DepartamentController(IDepartamentRepository departamentRepository)
    {
        this._departamentRepository = departamentRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetDepartaments()
    {
        try
        {
            return Ok(await _departamentRepository.GetDepartaments());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Departament>> GetDepartamentById(int id)
    {
        try
        {
            var result = await _departamentRepository.GetDepartamentById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("employee/{id:int}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeById(int id)
    {
        try
        {
            var employee = await _departamentRepository.GetEmployeeByDepartament(id);
            if (!employee.Any()) // Verifica se a lista de employee está vazia
            {
                return NotFound($"Nenhum empregado encontrado para o departament ID: {id}");
            }
            return Ok(employee);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Departament>> CreateDepartament([FromBody] Departament departament)
    {
        try
        {
            if (departament == null)
            {
                return BadRequest();
            }

            var createdDepartament = await _departamentRepository.AddDepartament(departament);
            return CreatedAtAction(nameof(GetDepartamentById), new { 
                Id = createdDepartament.Id }, 
                createdDepartament);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar dados no banco de dados");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Departament>> UpdateDepartament(int id, [FromBody] Departament departament)
    {
        try
        {
            if (departament == null)
            {
                return BadRequest("Dados do departament inválidos.");
            }

            departament.Id = id;

            var updatedDepartament = await _departamentRepository.UpdateDepartament(departament);
            if (updatedDepartament == null)
            {
                return NotFound($"Employee com ID {id} não foi encontrado.");
            }

            return Ok(updatedDepartament);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar dados no banco de dados");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Departament>> DeleteDepartament(int id)
    {
        try
        {
            var result = await _departamentRepository.GetDepartamentById(id);
            if (result == null)
            {
                return NotFound($"Departament com id = {id}, não foi encontrado");
            }
            _departamentRepository.DeleteDepartamentById(id);

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no banco de dados");
        }
    }
}