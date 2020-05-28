using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public class ShoppingCartModel
    {
        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public decimal SubTotal { get; set; }
        public List<CartItemsModel> CartItemsList { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class CartItemsModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductImagePath { get; set; }
        public int Quantity { get; set; }
    }
}