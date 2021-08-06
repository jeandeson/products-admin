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
    public class ClientesController : ControllerBase
    {
        private readonly dbprojetoprodutosContext _context;

        public ClientesController(dbprojetoprodutosContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbClientes>>> GetTbClientes()
        {
            return await _context.TbClientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbClientes>> GetTbClientes(int id)
        {
            var tbClientes = await _context.TbClientes.FindAsync(id);

            if (tbClientes == null)
            {
                return NotFound();
            }

            return tbClientes;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbClientes(int id, TbClientes tbClientes)
        {
            if (id != tbClientes.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(tbClientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbClientesExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TbClientes>> PostTbClientes(TbClientes tbClientes)
        {
            _context.TbClientes.Add(tbClientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbClientes", new { id = tbClientes.IdCliente }, tbClientes);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbClientes>> DeleteTbClientes(int id)
        {
            var tbClientes = await _context.TbClientes.FindAsync(id);
            if (tbClientes == null)
            {
                return NotFound();
            }

            _context.TbClientes.Remove(tbClientes);
            await _context.SaveChangesAsync();

            return tbClientes;
        }

        private bool TbClientesExists(int id)
        {
            return _context.TbClientes.Any(e => e.IdCliente == id);
        }
    }
}
