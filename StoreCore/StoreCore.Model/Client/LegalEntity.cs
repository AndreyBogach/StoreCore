using System;
using StoreCore.Model.Client.Interfaces;

namespace StoreCore.Model.Client
{
    public class LegalEntity: IClient, ILegalEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long TIN { get; set; }
        public Address.Address Address { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
