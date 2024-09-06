using Company.Models;
using Company.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers;

[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    

    [HttpGet]
    public async Task<ActionResult> GetEmployees()
    {
        try
        {
            return Ok(await _employeeRepository.GetEmployees());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        try
        {
            var result = await _employeeRepository.GetEmployeeById(id);
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

    [HttpPost]
    public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
    {
        try
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
            {
                return BadRequest("Dados inválidos.");
            }

            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { 
                Id = createdEmployee.Id }, 
                createdEmployee);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar no banco: " + ex);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(int id, [FromBody] Employee employee)
    {
        try
        {
            if (employee == null)
            {
                return BadRequest("Dados inválidos.");
            }

            employee.Id = id;

            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            if (updatedEmployee == null)
            {
                return NotFound($"Empregado com ID {id} não foi encontrado.");
            }

            return Ok(updatedEmployee);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar no banco: " + ex);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Employee>> DeleteEmployee(int id)
    {
        try
        {
            var result = await _employeeRepository.GetEmployeeById(id);
            if (result == null)
            {
                return NotFound($"Empregado com ID = {id} não encontrado.");
            }
            _employeeRepository.DeleteEmployeeById(id);

            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar dados no banco");
        }
    }
}