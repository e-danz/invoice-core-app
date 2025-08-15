using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceCoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesApiController(IInvoiceService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetAll()
            => Ok(await service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Invoice>> Get(int id)
        {
            var invoice = await service.GetByIdAsync(id);
            return invoice == null ? NotFound() : Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Post([FromBody] Invoice invoice)
        {
            await service.AddAsync(invoice);
            return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Invoice invoice)
        {
            if (id != invoice.Id) return BadRequest();
            await service.UpdateAsync(invoice);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
