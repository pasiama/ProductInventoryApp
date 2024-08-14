﻿namespace ProductInventoryApp.Models
{
    public class Suppliers
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public string Product { get; set; }
        public string Contact { get; set; }
        public string Category { get; set; }

        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string SupplyType { get; set; }

        public string SupplierId { get; set; }
    }
}
