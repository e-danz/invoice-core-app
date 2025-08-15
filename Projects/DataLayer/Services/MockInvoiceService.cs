using DataLayer.Models;

namespace DataLayer.Services
{
    public class MockInvoiceService : IInvoiceService
    {
        private readonly List<Invoice> _invoices = [];
        private int _nextId = 1;

        public MockInvoiceService()
        {
            _invoices.Add(new Invoice
            {
                Id = _nextId++,
                Description = "Office Supplies",
                DueDate = DateTime.Today.AddDays(30),
                Supplier = "Acme Corp",
                InvoiceLines =
                [
                    new InvoiceLine { Description = "Paper", Price = 5.0, Quantity = 10 },
                    new InvoiceLine { Description = "Pens", Price = 1.5, Quantity = 20 }
                ]
            });
        }

        public Task<List<Invoice>> GetAllAsync() => Task.FromResult(_invoices.ToList());

        public Task<Invoice?> GetByIdAsync(int id) => Task.FromResult(_invoices.FirstOrDefault(i => i.Id == id));

        public Task AddAsync(Invoice invoice)
        {
            invoice.Id = _nextId++;
            _invoices.Add(invoice);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Invoice invoice)
        {
            var idx = _invoices.FindIndex(i => i.Id == invoice.Id);
            if (idx >= 0)
                _invoices[idx] = invoice;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Id == id);
            if (invoice != null)
                _invoices.Remove(invoice);
            return Task.CompletedTask;
        }
    }
}
