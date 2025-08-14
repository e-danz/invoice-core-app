using Microsoft.EntityFrameworkCore;
using InvoiceCoreApp.Models;

namespace InvoiceCoreApp.DataLayer
{
    public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : DbContext(options)
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }
}