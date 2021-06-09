using System.Collections.Generic;
using System.Threading.Tasks;
using Cakes.Business;
using Cakes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cakes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CakesController : ControllerBase
    {
        private readonly ICakesRepository _cakesRepository;

        public CakesController(ICakesRepository cakesRepository)
        {
            _cakesRepository = cakesRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Cake>>> GetCakesAsync()
        {
            return Ok(await _cakesRepository.GetCakesAsync());
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cake>> GetCakeAsync(int id)
        {
            var cake = await _cakesRepository.GetCakeAsync(id);
            if(cake is null)
                return NotFound("Cake not found");
            return Ok(cake);
        }
        
        [HttpPost]
        public async Task<ActionResult<Cake>> GetCakeAsync(Cake cake)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (await _cakesRepository.CakeExistsAsync(cake.Name))
                return Conflict("Cake already exists");
            
            var item = await _cakesRepository.AddCakeAsync(cake);
            return CreatedAtAction("GetCake", new {id = item.Id} ,item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCakeAsync(int id, Cake cake)
        {
            if (id != cake.Id)
                return BadRequest("Id mismatch");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _cakesRepository.UpdateCakeAsync(cake);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveCakeAsync(int id)
        {
            await _cakesRepository.RemoveCakeAsync(id);
            return NoContent();
        }
    }
}