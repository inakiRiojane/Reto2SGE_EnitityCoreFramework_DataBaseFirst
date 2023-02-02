﻿using Reto2eSge_3__.Core.Entities;
using System;
using System.Collections.Generic;

namespace Reto2eSgeG3.Core.Entitis
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}


