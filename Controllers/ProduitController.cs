using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilterDemoApp.Models;
using System.Text.Json;
using FilterDemoApp.Filters;

namespace FilterDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
        // [AuthorFilter("Author", "Jason Mandabrandja")] First
    public class ProduitController : ControllerBase
    {
        private readonly ILogger<ProduitController> _logger;

        public ProduitController(ILogger<ProduitController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public JsonResult Create(Produit produit)
        {
            _logger.LogInformation("{0}", JsonSerializer.Serialize(produit));
            return new JsonResult(produit);
        }
        [HttpPut("{id:int}")]
        public JsonResult Edit(string id, Produit produit)
        {

            _logger.LogInformation("{0}", id);
            _logger.LogInformation("{0}", JsonSerializer.Serialize(produit));
            return new JsonResult(produit);
        }
        [HttpDelete("{id:int}")]
        public JsonResult Delete(string id)
        {

            _logger.LogInformation("{0}", id);
            return new JsonResult(id);
        }
        [HttpGet]
        // [AuthorFilter("Author", "Jason Mandabrandja")] First
        public IEnumerable<Produit> Get()
        =>
            new List<Produit>();

    }
}
