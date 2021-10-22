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
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public ProduitController(ILogger<ProduitController> logger,UserManager<IdentityUser> userManager, ApplicationDbContext dbContext,IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
            _configuration=configuration;
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
        // [Authorize(Policy="AdminOnly")]
        public IActionResult Get([FromQuery]int id){
            LoadBaseInfo();
            return new JsonResult(new {id=id});
        }
        // [HttpGet("{id}.{format?}")]
        // public Produit Get(int id)=> new Produit{ Id = $"{id}" };
        


private void LoadBaseInfo()
        {

            var listRoles = _configuration
                               .GetSection("ListRoles")
                               .GetChildren()
                               .Select(x => x.Value)
                               .ToArray();


            var section = _configuration.GetSection("userName");
            var userName = section.Get<string>();

            section = _configuration.GetSection("password");
            var password = section.Get<string>();

            try
            {
                var _ = _dbContext.Database.BeginTransactionAsync().Result;

                foreach (var item in listRoles)
                {
                    var test = _dbContext.Roles.Any(role => role.Name == item);
                    if (!test)
                    {
                        _dbContext.Roles.Add(new IdentityRole { Name = item, NormalizedName = item.Trim().ToUpper() });
                    }


                }
                _dbContext.SaveChanges();

                if (!_dbContext.Users.Any(u => u.UserName == userName))
                {
                    var currentUserSa = new IdentityUser { UserName = userName.Trim(), NormalizedUserName = userName.Trim().ToUpper() };
                    var result = _userManager.CreateAsync(currentUserSa, password).Result;

                    var currentRole = _dbContext.Roles.Where(x => x.Name == "Super Admin").FirstOrDefault() as IdentityRole;
                    var currentUserRole = new IdentityUserRole<string> { RoleId = currentRole.Id, UserId = currentUserSa.Id };
                    _dbContext.UserRoles.Add(currentUserRole);
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);

                try
                {
                    _dbContext.Database.RollbackTransaction();

                }
                catch (Exception exp1)
                {
                    Console.WriteLine(exp1.Message);
                }
            }


        }
 
    }
}
