using System.Collections.Generic;
using System.Threading.Tasks;
using AspCoreWebApi.Business.Services.ServiceContracts;
using AspCoreWebApi.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebApiSample.Controllers
{
    [Produces("application/json")]
    //[Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IEntityProvider _entityProvider;

        public ProductsController(IEntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }

        [HttpGet]
        [Route("Products")]
        public async Task<List<Product>> Get()
        {
            return await _entityProvider.GetAllAsync<Product>();
        }
    }
}