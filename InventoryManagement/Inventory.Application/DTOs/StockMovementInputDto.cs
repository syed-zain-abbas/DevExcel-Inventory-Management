using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs
{
    public class StockMovementInputDto
    {
        public int ProductId { get; set; }

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }
    }
}
