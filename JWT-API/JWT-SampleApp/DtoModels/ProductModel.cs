using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public class ProductModel
    {
        public int ProductId { get; set; }
      
        public string Category { get; set; }
        
        public string Name { get; set; }
        public decimal Price { get; set; }
       
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string ProductImagePath { get; set; }
    }
}