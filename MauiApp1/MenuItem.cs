using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ItemType { get; set; } // Add a property to specify the type of item (e.g., Food, Drink)
                                             // Add more properties as needed
    }
}
