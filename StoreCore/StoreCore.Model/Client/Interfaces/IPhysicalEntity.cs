using System;

namespace StoreCore.Model.Client.Interfaces
{
    public interface IPhysicalEntity
    {
        string PassportNumber { get; }
        DateTime? DateOfBirth { get; }
        Address.Address Address { get; }
    }
}
