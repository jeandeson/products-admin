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
    public class ClientesController : ControllerBase
    {
        private readonly ICustomLog _logger;
        private readonly ClienteService _clienteService;

        public ClientesController(ICustomLogFactory logger, ClienteService clienteService)
        {
            _logger = logger.CreateLogger<ICustomLogFactory>();
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public IActionResult Get(long? IdCliente = null)
        {
            List<Cliente> clientes;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);
                _logger.AddID("Salve", "Piva");

                clientes = _clienteService.Get(IdCliente: IdCliente);

                response = Ok(clientes);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Clientes/5
        [HttpGet("{IdCliente}", Name = "GetCliente")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long IdCliente)
        {
            Cliente cliente;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                cliente = _clienteService.Get(IdCliente);

                if (cliente != null)
                {
                    response = Ok(cliente);
                }
                else
                {
                    response = NotFound(string.Empty);
                }

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                cliente = _clienteService.Insert(cliente);

                response = Ok(cliente);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Clientes/5
        [HttpPut("{IdCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long IdCliente, [FromBody] Cliente cliente)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                cliente.IdCliente = IdCliente;
                cliente = _clienteService.Update(cliente);

                response = Ok(cliente);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{IdCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long IdCliente)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                _clienteService.Delete(IdCliente);

                response = Ok(string.Empty);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
