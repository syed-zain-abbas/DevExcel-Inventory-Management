using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs
{
    public class StockMovementDto
    {
        public int Id { get; set; }

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}
