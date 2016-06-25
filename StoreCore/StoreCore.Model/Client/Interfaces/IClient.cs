using System;

namespace StoreCore.Model.Client.Interfaces
{
    public interface IClient
    {
        int ClientId { get; }
        string Name { get; }
        string Type { get; }
        DateTime CreationDate { get; }
    }
}
