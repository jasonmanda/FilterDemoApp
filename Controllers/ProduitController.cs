using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilterDemoApp.Models;
using System.Text.Json;
using FilterDemoApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FilterDemoApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace FilterDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [FormatFilter]

    // [AuthorFilter("Author", "Jason Mandabrandja")] First
    public class ProduitController : ControllerBase
    {
        private readonly ILogger<ProduitController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public ProduitController(ILogger<ProduitController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        [HttpPost]
        public JsonResult Post([FromBody] Produit produit)
        {
            _logger.LogInformation("{0}", JsonSerializer.Serialize(produit));
            return new JsonResult(produit);
        }
        [HttpPut("{id:int}")]
        // [ClaimRequirement(MyClaimTypes.Permission, "CanReadResource")]
        public IActionResult Put([FromRoute] string id, [FromBody] Produit produit)
        {
            if (!ModelState.IsValid) return BadRequest(produit);
            _logger.LogInformation("{0}", id);
            _logger.LogInformation("{0}", JsonSerializer.Serialize(produit));
            return new JsonResult(produit);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(string id)
        {

            _logger.LogInformation("{0}", id);
            return new JsonResult(id);
        }
        // [HttpGet]
        // // [AuthorFilter("Author", "Jason Mandabrandja")] First
        // public IActionResult Get()
        // => NoContent();

        [HttpGet]
        [Authorize]
        // [Authorize(Roles = "Super Admin")]
        // [Authorize(Policy = "AdminOnly")]

        public async Task<IActionResult> Get([FromQuery] int id)
        {

           var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;
            return new JsonResult(new { Id=user?.UserName,UInt16=HttpContext.User.Identity.Name});
        }
        // [HttpGet("{id}.{format?}")]
        // public Produit Get(int id)=> new Produit{ Id = $"{id}" };



    }
}
