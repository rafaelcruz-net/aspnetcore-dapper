using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperSampleCRUD.Model;
using DapperSampleCRUD.Model.Repository;

namespace DapperSampleCRUD.Controllers
{
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private SupplierRepository Repository { get; set; }

        public SupplierController(SupplierRepository repository)
        {
            this.Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpPost]
        public IActionResult Post([FromBody] Supplier model)
        {
            this.Repository.Save(model);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Supplier model)
        {
            this.Repository.Update(model);
            return Ok();
        }

        [HttpDelete("{supplierId}")]
        public IActionResult Delete(int supplierId)
        {
            this.Repository.Delete(supplierId);
            return Ok();
        }

    }
}
