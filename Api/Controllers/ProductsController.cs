using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Product.Get")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getAll")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getlistbycategory/{categoryId}")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }


        //[HttpPut("add")]
        //public IActionResult Add(Product product)
        //{
        //    var result = _productService.Add(product);
        //    if (result.Success)
        //        return Ok(result.Message);
        //    else
        //        return BadRequest(result.Message);
        //}


        [HttpPost("transaction")]
        public IActionResult TransactionTest(Product product)
        {
            var result = _productService.TransactionalOperation(product);
            if (result.Success)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }

    }
}