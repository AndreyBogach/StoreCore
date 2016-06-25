using System.ComponentModel.DataAnnotations;

namespace StoreCore.Web.Models
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Count")]
        public int Count { get; set; }

        [Required]
        [Range(typeof(decimal), "0,00", "999999,99")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}