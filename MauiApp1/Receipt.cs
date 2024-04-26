using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Receipt
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // Adding Quantity property
        public decimal TotalPrice { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
    }
}
