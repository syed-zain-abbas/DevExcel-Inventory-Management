using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Common
{
    public class DuplicateSkuException : Exception
    {
        public DuplicateSkuException(Exception innerException)
            : base("This SKU already exists.", innerException)
        {
        }
    }
}
