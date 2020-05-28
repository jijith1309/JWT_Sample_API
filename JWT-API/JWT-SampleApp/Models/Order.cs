using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_SampleApp.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderId { get; set; }
        [MaxLength(100)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public decimal TotalOrderPrice { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(30)]
        public string  Country { get; set; }
        [MaxLength(30)]
        public string PostalCode { get; set; }
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [MaxLength(15)]
        public string OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }


    }
}