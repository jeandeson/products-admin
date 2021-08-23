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
    public class EnderecosController : ControllerBase
    {
        private readonly ICustomLog _logger;
        private readonly EnderecoService _enderecosService;

        public EnderecosController(ICustomLogFactory logger, EnderecoService enderecosService)
        {
            _logger = logger.CreateLogger<ICustomLogFactory>();
            _enderecosService = enderecosService;
        }

        // GET: api/Enderecos
        [HttpGet]
        public IActionResult Get(long? IdEndereco = null, long? IdCliente = null, long? IdCentro = null)
        {
            List<Endereco> enderecos;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);
                _logger.AddID("Salve", "Piva");

                enderecos = _enderecosService.Get(IdEndereco: IdEndereco, IdCliente: IdCliente, IdCentro: IdCentro);

                response = Ok(enderecos);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Enderecos/5
        [HttpGet("{IdEndereco}", Name = "GetEndereco")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long IdEndereco)
        {
            Endereco endereco;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                endereco = _enderecosService.Get(IdEndereco);

                if (endereco != null)
                {
                    response = Ok(endereco);
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
        public IActionResult Post([FromBody] Endereco endereco)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                endereco = _enderecosService.Insert(endereco);

                response = Ok(endereco);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Enderecos/5
        [HttpPut("{IdEndereco}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long IdEndereco, [FromBody] Endereco endereco)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                endereco.IdEndereco = IdEndereco;
                endereco = _enderecosService.Update(endereco);

                response = Ok(endereco);

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
        [HttpDelete("{IdEndereco}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long IdEndereco)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                _enderecosService.Delete(IdEndereco);

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
