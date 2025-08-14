using Microsoft.AspNetCore.Mvc.RazorPages;
using InvoiceCoreApp.Models;
using InvoiceCoreApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceCoreApp.Pages.Invoices
{
    public class IndexModel(IInvoiceService service) : PageModel
    {
        public List<Invoice> Invoices { get; set; } = new();

        public async Task OnGetAsync()
        {
            Invoices = await service.GetAllAsync();
        }
    }
}
    