using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Amount { get; set; }
    }
}
