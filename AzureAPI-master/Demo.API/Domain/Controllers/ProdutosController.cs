using Demo.API.Domain.Model;
using Demo.API.Domain.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RauchTech.Common.Logging;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Controllers
{
    [EnableCors("Policy1")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ICustomLog _logger;
        private readonly ProdutoService _produtoService;

        public ProdutosController(ICustomLogFactory logger, ProdutoService produtoService)
        {
            _logger = logger.CreateLogger<ProdutosController>();
            _produtoService = produtoService;
        }
        // GET: api/Produtos
        [HttpGet]
        public IActionResult Get(long? IdProduto = null)
        {
            List<Produto> produtos;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);
                _logger.AddID("Salve", "Piva");

                produtos = _produtoService.Get(IdProduto: IdProduto);

                response = Ok(produtos);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Produtos/5
        [HttpGet("{IdProduto}", Name = "GetProduto")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long IdProduto)
        {
            Produto produto;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                produto = _produtoService.Get(IdProduto);

                if (produto != null)
                {
                    response = Ok(produto);
                }
                else
                {
                    response = NotFound(string.Empty);
                }

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Produto produto)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                produto = _produtoService.Insert(produto);

                response = Ok(produto);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Produtos/5
        [HttpPut("{IdProduto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long IdProduto, [FromBody] Produto produto)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                produto.IdProduto = IdProduto;
                produto = _produtoService.Update(produto);

                response = Ok(produto);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{IdProduto}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long IdProduto)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                _produtoService.Delete(IdProduto);

                response = Ok(string.Empty);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
