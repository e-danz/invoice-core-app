using InvoiceCoreApp.DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InvoiceCoreApp.Models;

namespace InvoiceCoreApp.Pages.Invoices
{
    public class DetailsModel(IInvoiceService service) : PageModel
    {
        public Invoice? Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invoice = await service.GetByIdAsync(id);
            if (Invoice == null)
                return NotFound();
            return Page();
        }
    }
}