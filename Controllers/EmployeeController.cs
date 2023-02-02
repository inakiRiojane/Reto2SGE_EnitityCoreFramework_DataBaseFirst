using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSge_3__;
using Reto2eSge_3__.Core.Entities;

namespace Reto2eSgeG3.Core.Entitis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetUserByNameAndPass(string name, string pass)
        {
            var result = await _context.Employees
                .Where(x => x.FirstName.Contains(name)
                && x.Password.Contains(pass))
                .ToListAsync();


            return result; 
        }

        
    }
}
