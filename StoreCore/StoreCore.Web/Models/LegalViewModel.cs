using System;
using System.ComponentModel.DataAnnotations;

namespace StoreCore.Web.Models
{
    public class LegalViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "TIN")]
        public long TIN { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Home")]
        public string Home { get; set; }
    }
}