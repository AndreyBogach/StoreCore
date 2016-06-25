using System.ComponentModel.DataAnnotations;
using System;

namespace StoreCore.Web.Models
{
    public class PhysicalViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Passport { get; set; }

        public DateTime DateOfBirth { get; set; }

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