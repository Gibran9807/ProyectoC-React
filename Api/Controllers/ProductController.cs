using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataAccess.Interfaces;
using Api.Repositories.Interfaces;
using Api.Responses;
using Entities;
using Entities.Dto;
using Entities.Http;
using Microsoft.AspNetCore.Mvc;
//using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productrepository;

        public ProductController(IProductRepository productrepository)
        {
            _productrepository = productrepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = new Response<IEnumerable<Products>>();
            var products = await _productrepository.GetAllProducts();

            response.Data = products;

            return Ok(response);
        }

        [HttpGet]
        [Route("api/[controller]/search")]
        public  async Task<ActionResult<Response<IEnumerable<ProductDto>>>> SearchProduct([FromQuery] ProductFilter filter)
        {
            var response = new Response<IEnumerable<ProductDto>>();
            var product = await _productrepository.SearchByID(filter);
            response.Data = product;

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var response = new Response<Products>();
            var products = await _productrepository.GetOneProduct(id);

            if (products == null)
            {
                response.Message = "Product not found";
                response.Errors.Add("The Product was not found");

                return NotFound(response);
            }

            response.Data = products;


            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Products products)
        {
            if (products == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var created = await _productrepository.InsertProduct(products);
            
            return Created("api/products/" + products.ID, products);

        }

        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] Products products)
        {
            if (products == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _productrepository.UpdateProduct(products);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<bool>>> DeleteProduct(string id)
        {
            var response = new Response<bool>();
        
            response.Data = await _productrepository.DeleteProduct(id);

            return Ok(response);
            
        }

        


    }
}