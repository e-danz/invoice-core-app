using Microsoft.AspNetCore.Mvc.RazorPages;
using InvoiceCoreApp.Models;
using InvoiceCoreApp.Services;

namespace InvoiceCoreApp.Pages.Invoices
{
    public class IndexModel(IInvoiceService service) : PageModel
    {
        public List<Invoice> Invoices { get; set; } = [];

        public async Task OnGetAsync()
        {
            Invoices = await service.GetAllAsync();
        }
    }
}
    