using System;

namespace StoreCore.Model.Address.Interfaces
{
    public interface IAddress
    {
        string Country { get; }
        string City { get; }
        string Street { get; }
        string HomeNumber { get; }
        DateTime CreationDate { get; }
    }
}
