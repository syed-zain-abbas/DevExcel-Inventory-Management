using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } = null!;
    }
}
