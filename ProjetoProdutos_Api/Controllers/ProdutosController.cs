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
    public class ProdutosController : ControllerBase
    {
        private readonly dbprojetoprodutosContext _context;

        public ProdutosController(dbprojetoprodutosContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbProdutos>>> GetTbProdutos()
        {
            return await _context.TbProdutos.ToListAsync();
        }

        // GET: api/Produtos/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TbProdutos>> GetTbProdutos(int id)
        {
            var tbProdutos = await _context.TbProdutos.FindAsync(id);

            if (tbProdutos == null)
            {
                return NotFound();
            }

            return tbProdutos;
        }

        // PUT: api/Produtos/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbProdutos(int id, TbProdutos tbProdutos)
        {
            if (id != tbProdutos.IdProduto)
            {
                return BadRequest();
            }

            _context.Entry(tbProdutos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbProdutosExists(id))
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

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<TbProdutos>> PostTbProdutos(TbProdutos tbProdutos)
        {   
            _context.TbProdutos.Add(tbProdutos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbProdutos", new { id = tbProdutos.IdProduto }, tbProdutos);
        }

        // DELETE: api/Produtos/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbProdutos>> DeleteTbProdutos(int id)
        {
            var tbProdutos = await _context.TbProdutos.FindAsync(id);
            if (tbProdutos == null)
            {
                return NotFound();
            }

            _context.TbProdutos.Remove(tbProdutos);
            await _context.SaveChangesAsync();

            return tbProdutos;
        }

        private bool TbProdutosExists(int id)
        {
            return _context.TbProdutos.Any(e => e.IdProduto == id);
        }
    }
}
