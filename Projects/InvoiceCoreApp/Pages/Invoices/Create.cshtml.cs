using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InvoiceCoreApp.Models;
using System.Threading.Tasks;
using InvoiceCoreApp.DataLayer.Services;

namespace InvoiceCoreApp.Pages.Invoices
{
    public class CreateModel(IInvoiceService service) : PageModel
    {
        [BindProperty] public Invoice Invoice { get; set; } = new Invoice { InvoiceLines = [new InvoiceLine()] };

        public void OnGet()
        {
            if (Invoice.InvoiceLines.Count == 0)
                Invoice.InvoiceLines = [new InvoiceLine()];
        }

        public IActionResult OnPostAddLine()
        {
            Invoice.InvoiceLines.Add(new InvoiceLine());
            ModelState.Clear();
            return Page();
        }

        public IActionResult OnPostRemoveLine(int index)
        {
            if (Invoice.InvoiceLines.Count > index)
                Invoice.InvoiceLines.RemoveAt(index);
            if (Invoice.InvoiceLines.Count == 0)
                Invoice.InvoiceLines.Add(new InvoiceLine());
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            await service.AddAsync(Invoice);
            return RedirectToPage("Index");
        }
    }
}