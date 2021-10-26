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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FilterDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [FormatFilter]

    // [DebugResultFilter("Author", "Jason Mandabrandja")] First
    // [TypeFilter(typeof(DebugExceptionFilter))]
    public class ProduitController : ControllerBase
    {
        private readonly ILogger<ProduitController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        public ProduitController(ILogger<ProduitController> logger, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;

        }
        [HttpPost]
        [ServiceFilter(typeof(DebugActionFilter))]
        // [ServiceFilter(typeof(DebugActionFilter))]
        public JsonResult Post([FromBody] Produit produit)
        {
            throw new Exception("Failed");
            _logger.LogInformation("{0}", JsonSerializer.Serialize(produit));
            return new JsonResult(produit);
        }
        [HttpPut("{id:int}")]
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
        // // [DebugResultFilter("Author", "Jason Mandabrandja")] First
        // public IActionResult Get()
        // => NoContent();

        [HttpGet]
        [ServiceFilter(typeof(DebugAuthorizeFilter))]
        // [Authorize(Roles = "SuperAdmin")]
        // [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var userId = _userManager.GetUserId(User);
            var user1 = _userManager.GetUserAsync(User).Result;
            var user2 = await _userManager.GetUserAsync(User);
            return new JsonResult(new { User = user1, User1 = user2 });
        }
        // [HttpGet("{id}.{format?}")]
        // public Produit Get(int id)=> new Produit{ Id = $"{id}" };



    }
}
