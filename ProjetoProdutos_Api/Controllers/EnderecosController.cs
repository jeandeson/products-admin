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
    public class EnderecosController : ControllerBase
    {
        private readonly dbprojetoprodutosContext _context;

        public EnderecosController(dbprojetoprodutosContext context)
        {
            _context = context;
        }

        // GET: api/Enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbEnderecos>>> GetTbEnderecos()
        {
            return await _context.TbEnderecos.ToListAsync();
        }

        // GET: api/Enderecos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbEnderecos>> GetTbEnderecos(int id)
        {
            var tbEnderecos = await _context.TbEnderecos.FindAsync(id);

            if (tbEnderecos == null)
            {
                return NotFound();
            }

            return tbEnderecos;
        }

        // PUT: api/Enderecos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbEnderecos(int id, TbEnderecos tbEnderecos)
        {
            if (id != tbEnderecos.IdEndereco)
            {
                return BadRequest();
            }

            _context.Entry(tbEnderecos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbEnderecosExists(id))
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

        // POST: api/Enderecos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TbEnderecos>> PostTbEnderecos(TbEnderecos tbEnderecos)
        {
            _context.TbEnderecos.Add(tbEnderecos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbEnderecos", new { id = tbEnderecos.IdEndereco }, tbEnderecos);
        }

        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbEnderecos>> DeleteTbEnderecos(int id)
        {
            var tbEnderecos = await _context.TbEnderecos.FindAsync(id);
            if (tbEnderecos == null)
            {
                return NotFound();
            }

            _context.TbEnderecos.Remove(tbEnderecos);
            await _context.SaveChangesAsync();

            return tbEnderecos;
        }

        private bool TbEnderecosExists(int id)
        {
            return _context.TbEnderecos.Any(e => e.IdEndereco == id);
        }
    }
}
