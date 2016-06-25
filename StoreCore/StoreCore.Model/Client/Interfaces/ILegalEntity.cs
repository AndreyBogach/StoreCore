using System;

namespace StoreCore.Model.Client.Interfaces
{
    public interface ILegalEntity
    {
        long TIN { get; }
        Address.Address Address { get; }
    }
}
