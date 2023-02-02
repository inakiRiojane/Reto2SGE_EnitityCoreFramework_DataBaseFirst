using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reto2eSge_3__.Core.Models;
using Reto2eSgeG3.Core.Entitis;
using System.Net;

namespace Reto2eSge_3__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        private readonly IMapper _mapper;

        public CategoriasController(NorthwindContext northwindContext, IMapper mapper)
        {
            this._northwindContext = northwindContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryGetModel>> Get()
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


            return await _northwindContext.Categories
                     .ProjectTo<CategoryGetModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post(CategoryModel categoryPostModel)
        {

            var mapeado =  _mapper.Map<Category>(categoryPostModel);

            _northwindContext.Categories.Add(mapeado);

            await _northwindContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete (int id)
        {
            var category = await _northwindContext.Categories.FindAsync(id);
            
            if (category is null)
            {
                return NotFound();
            }

            _northwindContext.Remove(category);
            await _northwindContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryModel categoryPutModel)
        {

            var mapeado = _mapper.Map<Category>(categoryPutModel);
            var category = await _northwindContext.Categories.FindAsync(id);


            if (category == null)
            {
                return NotFound();
            }

            mapeado.CategoryName = category.CategoryName;
           // category.Description = mapeado.Description;

            await _northwindContext.SaveChangesAsync();
            return Ok();
        }


    }
}
