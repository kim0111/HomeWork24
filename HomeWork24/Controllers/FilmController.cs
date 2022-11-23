using HomeWork24.Data;
using HomeWork24.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {

        private readonly FilmContext _dbConext;

        public FilmController(FilmContext film)
        {
            _dbConext = film;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _dbConext.Films.ToListAsync();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _dbConext.Films.FindAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Film model)
        {
            if (model == null)
                return BadRequest();

            _dbConext.Films.Add(model);

            await _dbConext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _dbConext.Films.FindAsync(id);

            if (response == null)
                return NotFound();

            _dbConext.Films.Remove(response);

            await _dbConext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("Id")]
        public async Task<IActionResult> Put(int Id, Film film)
        {
            if (Id == film.Id)
            {

                var filmObject = _dbConext.Films.FindAsync(Id);

                try
                {
                    if (await filmObject != null)
                    {
                        _dbConext.Entry(filmObject).State = EntityState.Modified;
                        await _dbConext.SaveChangesAsync();
                    }
                    else
                    {
                        _dbConext.Films.Add(film);
                    }

                    await _dbConext.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (Id != film.Id)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }

            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpPatch]
        [Route("Id")]
        public async Task<IActionResult> Patch(int Id, Film film)
        {
            var fId = await _dbConext.Films.SingleAsync(x => x.Id == Id);


            fId.Name_Film = film.Name_Film;
            fId.Year = film.Year;
            fId.Profit = film.Profit;
            fId.MainCh = film.MainCh;

            await _dbConext.SaveChangesAsync();

            return Ok();
        }


    }

}

