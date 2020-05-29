using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels.Order
{
    public class OrderModel
    {
        public int OrderId { get; set; }
       
        public int UserId { get; set; }

        public decimal TotalOrderPrice { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
      
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required")]
        public string Country { get; set; }
        
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        public int CartId { get; set; }
        public string OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public List<OrderDetailsModel> OrderDetailsList { get; set; }
    }
}