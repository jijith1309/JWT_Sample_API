using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public class CartRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //[MinLength(0)]
        public int Quantity { get; set; }
        public int UserId { get; set; }

    }
}