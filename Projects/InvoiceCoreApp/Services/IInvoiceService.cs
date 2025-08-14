using InvoiceCoreApp.Models;

namespace InvoiceCoreApp.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
    }
}
