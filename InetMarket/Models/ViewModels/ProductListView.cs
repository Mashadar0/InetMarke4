﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    //для сортировки продуктов по категориям 
    public class ProductListView
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Categories { get; set; }
    }
}
