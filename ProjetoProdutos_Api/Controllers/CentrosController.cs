using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoProdutos_Api;

namespace ProjetoProdutos_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentrosController : ControllerBase
    {
        private readonly dbprojetoprodutosContext _context;

        public CentrosController(dbprojetoprodutosContext context)
        {
            _context = context;
        }

        // GET: api/Centros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbCentros>>> GetTbCentros()
        {
            return await _context.TbCentros.ToListAsync();
        }

        // GET: api/Centros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbCentros>> GetTbCentros(int id)
        {
            var tbCentros = await _context.TbCentros.FindAsync(id);

            if (tbCentros == null)
            {
                return NotFound();
            }

            return tbCentros;
        }

        // PUT: api/Centros/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbCentros(int id, TbCentros tbCentros)
        {
            if (id != tbCentros.IdCentro)
            {
                return BadRequest();
            }

            _context.Entry(tbCentros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbCentrosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Centros
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TbCentros>> PostTbCentros(TbCentros tbCentros)
        {
            _context.TbCentros.Add(tbCentros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbCentros", new { id = tbCentros.IdCentro }, tbCentros);
        }

        // DELETE: api/Centros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbCentros>> DeleteTbCentros(int id)
        {
            var tbCentros = await _context.TbCentros.FindAsync(id);
            if (tbCentros == null)
            {
                return NotFound();
            }

            _context.TbCentros.Remove(tbCentros);
            await _context.SaveChangesAsync();

            return tbCentros;
        }

        private bool TbCentrosExists(int id)
        {
            return _context.TbCentros.Any(e => e.IdCentro == id);
        }
    }
}
