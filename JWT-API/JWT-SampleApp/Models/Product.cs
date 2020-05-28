using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }
        [MaxLength(20)]
        public string Category { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string ProductImagePath { get; set; }
    }
}