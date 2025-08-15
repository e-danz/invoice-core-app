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

        public async Task<List<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Invoices.Include(i => i.InvoiceLines).ToListAsync(cancellationToken);
        }

        public async Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Invoices.Include(i => i.InvoiceLines)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task AddAsync(Invoice invoice, CancellationToken cancellationToken = default)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default)
        {
            _db.Invoices.Update(invoice);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var invoice = await _db.Invoices.FindAsync([id], cancellationToken);
            if (invoice != null)
            {
                _db.Invoices.Remove(invoice);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}