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
    public class UsersController : ControllerBase
    {
        private readonly ICustomLog _logger;
        private readonly UserService _userService;

        public UsersController(ICustomLogFactory logger, UserService userService)
        {
            _logger = logger.CreateLogger<ICustomLogFactory>();
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult Get(long? Id = null)
        {
            List<User> users;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);
                _logger.AddID("Salve", "Piva");

                users = _userService.Get(Id: Id);

                response = Ok(users);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/users/5
        [HttpGet("{email}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string email)
        {
            User user;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                user = _userService.Get(email);

                if (user != null)
                {
                    response = Ok(user);
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
        public IActionResult Post([FromBody] User user)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                user = _userService.Insert(user);

                response = Ok(user);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Users/5
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long Id, [FromBody] User user)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                user.Id = Id;
                user = _userService.Update(user);

                response = Ok(user);

                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex); ;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api/users/5
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long Id)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: ICustomLog.Begin);

                _userService.Delete(Id);

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
