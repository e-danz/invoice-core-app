using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services
{
    public class SqLiteInvoiceService : IInvoiceService
    {
        private readonly InvoiceDbContext _db;

        public SqLiteInvoiceService(InvoiceDbContext db)
        {
            _db = db;
            _db.Database.EnsureCreated(); // Code First !
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _db.Invoices.Include(i => i.InvoiceLines).ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _db.Invoices.Include(i => i.InvoiceLines).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _db.Invoices.Update(invoice);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _db.Invoices.Remove(invoice);
                await _db.SaveChangesAsync();
            }
        }
    }
}