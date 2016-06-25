using System;
using StoreCore.Model.Client.Interfaces;

namespace StoreCore.Model.Client
{
    public class Client: IClient
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
