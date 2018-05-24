using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Warehouse.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        //public Product()
        //{
        //    new StringLengthAttribute(100) { MinimumLength = 3 }; // Samma som StringLength-annotationen nedan (bara som exempel - ej körbart!)
        //}

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [UIHint("Currency")] //Generic for the item, uses DisplayTemplate "Currency.cshtml". Can also be hard coded into i.e. index.cshtml
        [Display(Name="How much to pay")]
        [Range(1, Int32.MaxValue, ErrorMessage = "The price must be higher than 1 SEK")]
        public int Price { get; set; }

        [Display(Name="Amount in store")]
        public int Quantity { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }
    }
}