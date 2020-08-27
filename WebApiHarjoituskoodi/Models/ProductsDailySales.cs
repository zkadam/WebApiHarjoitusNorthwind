using System;
using System.Collections.Generic;

namespace WebApiHarjoituskoodi.Models
{
    public partial class ProductsDailySales
    {
        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public double? DailySales { get; set; }
    }
}
