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
    public class CentrosController : ControllerBase
    {
        private readonly ICustomLog _logger;
        private readonly CentroService _centroService;

        public CentrosController(ICustomLogFactory logger, CentroService centrosService)
        {
            _logger = logger.CreateLogger<ICustomLogFactory>();
            _centroService = centrosService;
        }

        [HttpGet]
        public IActionResult Get(long? IdCentro = null)
        {
            List<Centro> centros;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);
                _logger.AddID("Salve", "Piva");

                centros = _centroService.Get(IdCentro: IdCentro);

                response = Ok(centros);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Centros/5
        [HttpGet("{IdCentro}", Name = "GetCentro")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long IdCentro, long name)
        {
            Centro centro;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                centro = _centroService.Get(IdCentro, name);

                if (centro != null)
                {
                    response = Ok(centro);
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
        public IActionResult Post([FromBody] Centro centro)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                centro = _centroService.Insert(centro);

                response = Ok(centro);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Centros/5
        [HttpPut("{IdCentro}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long IdCentro, [FromBody] Centro centro)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                centro.IdCentro = IdCentro;
                centro = _centroService.Update(centro);

                response = Ok(centro);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api//5
        [HttpDelete("{IdCentro}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long IdCentro)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                _centroService.Delete(IdCentro);

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
