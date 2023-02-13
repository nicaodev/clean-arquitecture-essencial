using CleanArchMvc.Application.Dtos;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            if (categories == null) return NotFound("Categories Not Found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]

        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var cat = await _categoryService.GetByIdAsync(id);
            if (cat == null) return NotFound("Categorie Not Found");
            return Ok(cat);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest("Invalid Data");

            await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id) return BadRequest();

            if (categoryDTO == null) return BadRequest();

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var cat = await _categoryService.GetByIdAsync(id);

            if (cat == null) return NotFound("Categorie not found");

            await _categoryService.Remove(id);

            return Ok(cat);
        }
    }
}