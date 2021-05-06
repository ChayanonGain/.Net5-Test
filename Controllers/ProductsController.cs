using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_hero.Entities;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using dotnet_hero.DTOs.Product;
using dotnet_hero.Interfaces;
using System.Net;

namespace dotnet_hero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productservice;
        public ProductsController(IProductService productservice)
        {
            this.productservice = productservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProduct()
        {   
            var map = new List<ProductResponse>();
            var result = (await productservice.GetAllProduct()).Adapt(map).ToList();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetOneProduct(int id)
        {
            var OneProduct = await productservice.GetProductByID(id);
            if (OneProduct == null)
            {
                return NotFound();
            }
            return OneProduct.Adapt<ProductResponse>();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductRequest productRequest)
        {
            var result = productRequest.Adapt<Product>();
            await productservice.Create(result);
            
            return StatusCode((int)(HttpStatusCode.Created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromForm] ProductRequest productRequest)
        {
            if (id != productRequest.ProductId)
            {
                return BadRequest();
            }
            var product = await productservice.GetProductByID(id);

            if (product == null)
            {
                return NotFound();
            }
            productRequest.Adapt(product);
            await productservice.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await productservice.GetProductByID(id);

            if (product == null)
            {
                return NotFound();
            }
            await productservice.Delete(product);
            return NoContent();
        }
    }
}
