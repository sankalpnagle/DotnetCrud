using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapp.Data;
using Webapp.Model;

namespace Webapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeApiDbContext dbContext;

        public EmployeeController(EmployeeApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            return Ok(await dbContext.Employees.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Address = addEmployeeRequest.Address,
                Phone = addEmployeeRequest.Phone,

            };
           await dbContext.Employees.AddAsync(employee); 
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if(employee != null)
            {
                employee.Name= updateEmployeeRequest.Name;
                employee.Email= updateEmployeeRequest.Email;    
                employee.Address= updateEmployeeRequest.Address;
                employee.Phone= updateEmployeeRequest.Phone;  
                
                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        { 
        var employee = await dbContext.Employees.FindAsync(id); 
            if(employee != null)
            {
                dbContext.Remove(employee);
               await dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }
    }
}
