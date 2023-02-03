using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSge_3__;
using Reto2eSge_3__.Core.Entities;
using Reto2eSge_3__.Core.Models;
using System.Security.Cryptography;
using System.Text;
using NorthwindContext = Reto2eSge_3__.NorthwindContext;

namespace Reto2eSgeG3.Core.Entitis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(NorthwindContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet("GetUser&Pass")]
        public async Task<IEnumerable<Employee>> GetUserByNameAndPass(string name, string pass)
        {
            var contra = ComputeSha256Hash(pass);
            return await _context.Employees
                .Where(x => x.FirstName.Contains(name)
                && x.Password.Contains(pass))
                .ToListAsync();
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpGet("GetDate")]
        public async Task<IEnumerable<EmployeeGetModel>> GetUserDateMoreThan(DateTime date)
        {
            var partial = await _context.Employees
                .Where(x => x.BirthDate > date)
                .ToListAsync();
            var result = _mapper.Map<List<EmployeeGetModel>>(partial);

            return result;
        }
    }


}

