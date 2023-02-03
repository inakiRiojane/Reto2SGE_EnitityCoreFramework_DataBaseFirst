
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reto2eSge_3__.Core.Entities;
using Reto2eSge_3__.Core.Models;
using Reto2eSge_3__.Paginador;

namespace Reto2eSge_3__.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class ClientesController : ControllerBase
    {
        private readonly int _RegistrosPorPagina = 5;
        //private PaginadorGenerico<Customer> _PaginadorCustomers;
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;



        public ClientesController(NorthwindContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("Paises")]
        public async Task<IEnumerable<ClientesModel>> Get()
        {

            return await _context.Customers
                .Select(cli => new ClientesModel()
                {
                    Country = cli.Country
                })
                   .Distinct()
                   .OrderByDescending(cli => cli.Country)
                   .ToListAsync();


        }

        [HttpGet("filtrarPais")]

        public async Task<IEnumerable<Customer>> FiltrarValor(string Valor)
        {
            

            return await _context.Customers
                .Where(Customers => Customers.Country.Contains(Valor))
                .OrderBy(Customers => Customers.CompanyName)
                .ThenBy(Customers => Customers.ContactName)
                .ToArrayAsync();


        }


        [HttpGet("Varias ciudades")]
        public IActionResult GetCustomersByCountry(string[] countries)
        {
            if (countries == null || countries.Length == 0)
            {
                return Ok(_context.Customers);
            }

            var customers = _context.Customers.Where(c => countries.Contains(c.Country));
            return Ok(customers);

        }



        [HttpGet("Custom info")]
        public async Task<IEnumerable<ClientesCustomInfoModel>> GetCustomInfo( int pageSizeFiveTenOrFifteen = 5)
        {

                return await _context.Customers
                    .ProjectTo<ClientesCustomInfoModel>(_mapper.ConfigurationProvider)
                           .Skip((1 - 1) * pageSizeFiveTenOrFifteen)
                           .Take(pageSizeFiveTenOrFifteen)
                           .ToListAsync();  
           
        }
    }
}

