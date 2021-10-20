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

namespace FilterDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [FormatFilter]

    // [AuthorFilter("Author", "Jason Mandabrandja")] First
    public class ProduitController : ControllerBase
    {
        private readonly ILogger<ProduitController> _logger;

        public ProduitController(ILogger<ProduitController> logger)
        {
            _logger = logger;
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
        [Authorize(Policy="AdminOnly")]
        public IActionResult Get([FromQuery]int id){
            return new JsonResult(new {id=id});
        }
        // [HttpGet("{id}.{format?}")]
        // public Produit Get(int id)=> new Produit{ Id = $"{id}" };
        

    }
}
