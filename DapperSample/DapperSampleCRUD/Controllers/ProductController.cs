using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperSampleCRUD.Model.Repository;

namespace DapperSampleCRUD.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ProductRepository Repository { get; set; }

        public ProductController(ProductRepository repository)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.Repository.GetProducts());
        }

        [HttpGet("{productId}")]

        public IActionResult Get(int productId)
        {
            return Ok(this.Repository.GetProductById(productId));
        }

        [HttpGet("supplier/{supplierId}")]
        public IActionResult GetProductBySupplier(int supplierId)
        {
            return Ok(this.Repository.GetProductBySupplier(supplierId));
        }

    }
}
