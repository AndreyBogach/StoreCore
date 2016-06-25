using System;
using StoreCore.Model.Address.Interfaces;

namespace StoreCore.Model.Address
{
    public class Address: IAddress
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
