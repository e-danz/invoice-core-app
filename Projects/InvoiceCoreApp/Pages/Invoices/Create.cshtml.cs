using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InvoiceCoreApp.Models;
using System.Threading.Tasks;
using InvoiceCoreApp.DataLayer.Services;

namespace InvoiceCoreApp.Pages.Invoices
{
    public class CreateModel(IInvoiceService service) : PageModel
    {
        [BindProperty]
        public Invoice Invoice { get; set; } = new Invoice
            { InvoiceLines = [new InvoiceLine()] };

        public void OnGet()
        {
            // Optionally initialize more lines?
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            await service.AddAsync(Invoice);
            return RedirectToPage("Index");
        }
    }
}