﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class OrderCartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public int TableNumber { get; set; }
    }
}
