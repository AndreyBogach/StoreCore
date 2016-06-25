using System.Collections.Generic;
using StoreCore.Common;
using StoreCore.Model.Client;

namespace StoreCore.Services.Client.Interfaces
{
    public interface IClientService
    {
        OperationResult AddClient(int type, string name);
        OperationResult AddLegalEntity(LegalEntity client);
        OperationResult AddPhysicalEntity(PhysicalEntity client);

        void UpdateLegalClient(LegalEntity client);
        void UpdatePhysicalClient(PhysicalEntity client);

        IEnumerable<LegalEntity> GetAllLegalClients();
        IEnumerable<PhysicalEntity> GetAllPhysicalClients();
        IEnumerable<Model.Client.Client> GetAllClients();

        LegalEntity GetLegalClient(int client_id);
        PhysicalEntity GetPhysicalClient(int client_id);

        int ClientType(int client_id);
        void Delete(int client_id);
    }
}
