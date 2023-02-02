using System;
using System.Collections.Generic;

namespace Reto2eSge_3__.Core.Entities.DbViews
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
