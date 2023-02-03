using System;
using System.Collections.Generic;

namespace Reto2eSge_3__.Entities
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
