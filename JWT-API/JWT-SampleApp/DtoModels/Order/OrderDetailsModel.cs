using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels.Order
{
    public class OrderDetailsModel
    {
        public int OrderDetailsId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Product id is needed")]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity must be greater than 0")]
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductImagePath { get; set; }
        public int OrderId { get; set; }
    }
}