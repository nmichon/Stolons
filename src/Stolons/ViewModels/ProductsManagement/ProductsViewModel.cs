﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stolons.Models;

namespace Stolons.ViewModels.ProductsManagement
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; private set; }
        public Stolon Stolon { get; private set; }

        public ProductsViewModel(IEnumerable<Product> products, Stolon stolon)
        {
            Products = products;
            Stolon = stolon;
        }
    }
}
