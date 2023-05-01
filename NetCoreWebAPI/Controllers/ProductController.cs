using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.DTOs;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _products;
      
        public ProductController(IProducts products)
        {
            this._products = products;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var list = await _products.GetAll();
                return Ok(list);
            }
            catch(Exception ex)
            {
                return Problem();
            }
        }


        [HttpPost]
        [Route("CreateOrUpdate")]
        public IActionResult CreateOrUpdate(ProductsDTO objProductDTO)
        {
            try
            {
                
                var data =  _products.CreateOrUpdateProduct(objProductDTO);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem("Something went wrong");
            }
        }

        [HttpGet]
        [Route("GetProductByID/{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            throw new Exception("Fake Exceptipn");

            var data = await _products.GetProductByID(id);
            return Ok(data);
        }
    }
}
