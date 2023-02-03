
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
        private PaginadorGenerico<Customer> _PaginadorCustomers;
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;



        public ClientesController(NorthwindContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }



        /*Crear una acción para recuperar todos los países existentes en la tabla Clientes de
ordenados descendentemente por nombre. En la lista resultante no pueden existir
registros duplicados.*/


        [HttpGet]
        public async Task<IEnumerable<ClientesModel>> Get()
        {
            // -- MAPEADO --
            //var algo = _northwindContext.Categories.ToList();

            //var algo2 = new List<CategoryViewModel>();

            //foreach (var a in algo)
            //{
            //    algo2.Add(new CategoryViewModel()
            //    {
            //        CategoryName = a.CategoryName,
            //        Description = a.Description
            //    });
            //}


            return await _context.Customers
                     .ProjectTo<ClientesModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }




        /*Crear una acción para recuperar todos clientes para un determinado país. El resultado
        tiene que estar ordenado por “CompanyName” y después por “ContactName”. No se
        ebe devolver la columna “CustomerID” en el resultado.*/

        [HttpGet("filtrarvalor")]

        public async Task<IEnumerable<Customer>> FiltrarValor(string Valor)
        {
            /*  var aux = _context.Customers
                  .Where(genero => genero.City.Contains(Valor))
                  .OrderByDescending(genero => genero.City);


              var auxLq = (from Customers in _context.Customers
                           where Customers.City.Contains(Valor)
                           orderby Customers.CompanyName
                           select Customers).ToList();*/

            return await _context.Customers
                .Where(Customers => Customers.City.Contains(Valor))
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

