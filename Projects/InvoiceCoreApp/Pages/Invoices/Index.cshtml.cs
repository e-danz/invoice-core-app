using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
    