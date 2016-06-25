using System;
using StoreCore.Model.Client.Interfaces;

namespace StoreCore.Model.Client
{
    public class PhysicalEntity: IClient, IPhysicalEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Address.Address Address { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
