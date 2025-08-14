using System;
using System.Collections.Generic;

namespace InvoiceCoreApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Description { get; init; } = string.Empty;
        public DateTime DueDate { get; init; }
        public string Supplier { get; init; } = string.Empty;
        public List<InvoiceLine> InvoiceLines { get; init; } = [];
    }

    public class InvoiceLine
    {
        public string Description { get; init; } = string.Empty;
        public double Price { get; init; }
        public int Quantity { get; init; }
    }
}
