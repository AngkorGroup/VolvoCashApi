﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.Application.MainContext.Clients.Services;

namespace VolvoCash.DistributedServices.MainContext.ApiWeb
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api_web/[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ClientsController : ControllerBase
    {
        #region Members
        private readonly IClientAppService _clientAppService;
        #endregion

        #region Constructor
        public ClientsController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<ActionResult> GetClients([FromQuery] string query = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageLength = 10)
        {
            var clients = await _clientAppService.GetClients(query, pageIndex, pageLength);
            return Ok(clients);
        }
        #endregion
    }
}
