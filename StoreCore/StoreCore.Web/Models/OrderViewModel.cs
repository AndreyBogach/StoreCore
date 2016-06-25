using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StoreCore.Web.Models
{
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "Client")]
        public int Client { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int Product { get; set; }

        [Required]
        [Display(Name = "Count")]
        public int Count { get; set; }

        public string ClientName { get; set; }
    }
}