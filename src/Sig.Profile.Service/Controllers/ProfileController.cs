using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Sig.Profile.Domain.Models.RequestModels.Profile;
using Sig.Profile.Application.Common;

namespace Sig.Profile.Service.Controllers
{
    /// <summary>
    /// Profiles allows CRUD for user data
    /// </summary>
    [Route("v1/Sigos/[controller]")]
    [Produces("applicaton/json")]    
    public class ProfileController : ControllerBase
    {
        protected readonly ILogger _logger;
        //private readonly 

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add New Profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult> AddProfile([FromBody] CreateProfileRequestDto request)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            _logger.LogDebug(Utility.LOG_ENTRY, methodName);

            //var result = await _


            return Ok();
        }
    }

}