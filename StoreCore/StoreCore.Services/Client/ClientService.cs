using System;
using System.Collections.Generic;
using System.Linq;
using StoreCore.Common;
using StoreCore.Data;
using StoreCore.Model.Address;
using StoreCore.Model.Client;
using StoreCore.Services.Client.Interfaces;

namespace StoreCore.Services.Client
{
    //record_state = 0 created item
    //record_state = 1 deleted item
    //record_state = 2 updated item
    public class ClientService : IClientService
    {
        public OperationResult AddClient(int type, string name)
        {
            var result = OperationResult.CreateWithSuccess();

            using (var context = new StoreCoreContext())
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var dbClient = context.inf_client.Create();

                    dbClient.name = name;
                    dbClient.type_id = type;
                    dbClient.creation_date = DateTime.UtcNow;
                    dbClient.record_updated = DateTime.UtcNow;
                    dbClient.record_state = 0;

                    context.inf_client.Add(dbClient);
                    context.SaveChanges();

                    result.Data.Add("id", dbClient.id);
                }
                else { return OperationResult.CreateWithError("Name is null"); }
            }
            return result;
        }
        public OperationResult AddLegalEntity(LegalEntity client)
        {
            var result = OperationResult.CreateWithSuccess();

            using (var context = new StoreCoreContext())
            {
                var newFacility = context.inf_legal_entity.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                if (newFacility != null)
                    return OperationResult.CreateWithError("Account is exist!");

                else
                {
                    var address = context.inf_address.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                    if(address != null)
                        return OperationResult.CreateWithError("Address is exist!");

                    var dbAddress = context.inf_address.Create();

                    dbAddress.client_id = client.ClientId;
                    dbAddress.country = client.Address.Country;
                    dbAddress.city = client.Address.City;
                    dbAddress.street = client.Address.Street;
                    dbAddress.home_number = client.Address.HomeNumber;
                    dbAddress.creation_date = DateTime.UtcNow;
                    dbAddress.record_updated = DateTime.UtcNow;
                    dbAddress.record_state = 0;

                    context.inf_address.Add(dbAddress);
                    context.SaveChanges();

                    var dbClient = context.inf_legal_entity.Create();

                    dbClient.client_id = client.ClientId;
                    dbClient.tin = client.TIN;
                    dbClient.address = dbAddress.id;
                    dbClient.creation_date = DateTime.UtcNow;
                    dbClient.record_updated = DateTime.UtcNow;
                    dbClient.record_state = 0;

                    context.inf_legal_entity.Add(dbClient);
                    context.SaveChanges();
                }

            }
            return result;
        }

        public OperationResult AddPhysicalEntity(PhysicalEntity client)
        {
            var result = OperationResult.CreateWithSuccess();

            using (var context = new StoreCoreContext())
            {
                var newEntity = context.inf_physical_entity.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                if (newEntity != null)
                    return OperationResult.CreateWithError("Account is exist!");

                else
                {
                    var address = context.inf_address.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                    if (address != null)
                        return OperationResult.CreateWithError("Address is exist!");

                    var dbAddress = context.inf_address.Create();

                    dbAddress.client_id = client.ClientId;
                    dbAddress.country = client.Address.Country;
                    dbAddress.city = client.Address.City;
                    dbAddress.street = client.Address.Street;
                    dbAddress.home_number = client.Address.HomeNumber;
                    dbAddress.creation_date = DateTime.UtcNow;
                    dbAddress.record_updated = DateTime.UtcNow;
                    dbAddress.record_state = 0;

                    context.inf_address.Add(dbAddress);
                    context.SaveChanges();

                    var dbClient = context.inf_physical_entity.Create();

                    dbClient.client_id = client.ClientId;
                    dbClient.date_of_birth = client.DateOfBirth;
                    dbClient.passport_number = client.PassportNumber;
                    dbClient.address = dbAddress.id;
                    dbClient.creation_date = DateTime.UtcNow;
                    dbClient.record_updated = DateTime.UtcNow;
                    dbClient.record_state = 0;

                    context.inf_physical_entity.Add(dbClient);
                    context.SaveChanges();
                }

            }
            return result;
        }

        public void Delete(int client_id)
        {
            using (var context = new StoreCoreContext())
            {
                var dbClient = context.inf_client.FirstOrDefault(f => f.id == client_id && f.record_state != 1);
                var dbAddress = context.inf_address.FirstOrDefault(f => f.client_id == client_id && f.record_state != 1);

                if(dbClient.type_id == 1)
                {
                    var dbFacility = context.inf_legal_entity.FirstOrDefault(f => f.client_id == client_id && f.record_state != 1);

                    if(dbFacility != null)
                    {
                        dbFacility.record_state = 1;
                        dbFacility.record_updated = DateTime.UtcNow;

                        context.SaveChanges();
                    }
                }

                if(dbClient.type_id == 2)
                {
                    var dbPhysical = context.inf_physical_entity.FirstOrDefault(f => f.client_id == client_id && f.record_state != 1);

                    if(dbPhysical != null)
                    {
                        dbPhysical.record_state = 1;
                        dbPhysical.record_updated = DateTime.UtcNow;

                        context.SaveChanges();
                    }
                }

                if (dbClient != null)
                {
                    dbClient.record_state = 1;
                    dbClient.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                } 
                
                if(dbAddress != null)
                {
                    dbAddress.record_state = 1;
                    dbAddress.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }             
            }
        }

        public IEnumerable<LegalEntity> GetAllLegalClients()
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_legal_entity.Where(w => w.record_state != 1)
                    .OrderByDescending(o => o.creation_date)
                    .Select(s => new LegalEntity
                    {
                        ClientId = s.client_id,
                        Name = s.inf_client.name,
                        Type = s.inf_client.inf_type.name,
                        TIN = s.tin,
                        Address = new Address()
                        {
                            City = s.inf_address.city,
                            Country = s.inf_address.country,
                            Street = s.inf_address.street,
                            HomeNumber = s.inf_address.home_number
                        },
                        CreationDate = s.creation_date
                    }).ToList();
            }
        }

        public IEnumerable<PhysicalEntity> GetAllPhysicalClients()
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_physical_entity.Where(w => w.record_state != 1)
                    .OrderByDescending(o => o.creation_date)
                    .Select(s => new PhysicalEntity
                    {
                        ClientId = s.client_id,
                        Name = s.inf_client.name,
                        Type = s.inf_client.inf_type.name,
                        DateOfBirth = s.date_of_birth,
                        PassportNumber = s.passport_number,
                        Address = new Address()
                        {
                            City = s.inf_address.city,
                            Country = s.inf_address.country,
                            Street = s.inf_address.street,
                            HomeNumber = s.inf_address.home_number
                        },
                        CreationDate = s.creation_date
                    }).ToList();
            }
        }

        public IEnumerable<Model.Client.Client> GetAllClients()
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_client.Where(w => w.record_state != 1)
                    .Select(s => new Model.Client.Client
                    {
                        ClientId = s.id,
                        Name = s.name,
                        Type = s.inf_type.name
                    }).ToList();
            }
        }

        public LegalEntity GetLegalClient(int client_id)
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_legal_entity.Where(w => w.client_id == client_id && w.record_state != 1)
                    .Select(s => new LegalEntity
                    {
                        ClientId = s.client_id,
                        Name = s.inf_client.name,
                        Type = s.inf_client.inf_type.name,
                        TIN = s.tin,
                        Address = new Address()
                        {
                            City = s.inf_address.city,
                            Country = s.inf_address.country,
                            Street = s.inf_address.street,
                            HomeNumber = s.inf_address.home_number
                        }
                    }).FirstOrDefault();
            }
        }

        public PhysicalEntity GetPhysicalClient(int client_id)
        {
            using (var context = new StoreCoreContext())
            {
                return context.inf_physical_entity.Where(w => w.client_id == client_id && w.record_state != 1)
                    .Select(s => new PhysicalEntity
                    {
                        ClientId = s.client_id,
                        Name = s.inf_client.name,
                        Type = s.inf_client.inf_type.name,
                        DateOfBirth = s.date_of_birth,
                        PassportNumber = s.passport_number,
                        Address = new Address()
                        {
                            City = s.inf_address.city,
                            Country = s.inf_address.country,
                            Street = s.inf_address.street,
                            HomeNumber = s.inf_address.home_number
                        }
                    }).FirstOrDefault();
            }
        }

        public int ClientType(int client_id)
        {
            using (var context = new StoreCoreContext())
            {
                var res = context.inf_client.FirstOrDefault(f => f.id == client_id && f.record_state != 1);

                if (res == null)
                    return 0;

                return res.type_id;
            }
        }
        public void UpdateLegalClient(LegalEntity client)
        {
            using (var context = new StoreCoreContext())
            {
                var dbClient = context.inf_client.FirstOrDefault(f => f.id == client.ClientId && f.record_state != 1);
                var dbAddress = context.inf_address.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);
                var dbFacility = context.inf_legal_entity.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                if (dbFacility != null)
                {
                    dbFacility.tin = client.TIN;
                    dbFacility.record_state = 2;
                    dbFacility.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }

                if (dbClient != null)
                {
                    dbClient.name = client.Name;
                    dbClient.record_state = 2;
                    dbClient.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }

                if (dbAddress != null)
                {
                    dbAddress.country = client.Address.Country;
                    dbAddress.city = client.Address.City;
                    dbAddress.street = client.Address.Street;
                    dbAddress.home_number = client.Address.HomeNumber;
                    dbAddress.record_state = 2;
                    dbAddress.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }
            }
        }

        public void UpdatePhysicalClient(PhysicalEntity client)
        {
            using (var context = new StoreCoreContext())
            {
                var dbClient = context.inf_client.FirstOrDefault(f => f.id == client.ClientId && f.record_state != 1);
                var dbAddress = context.inf_address.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);
                var dbPhysicalItem = context.inf_physical_entity.FirstOrDefault(f => f.client_id == client.ClientId && f.record_state != 1);

                if (dbPhysicalItem != null)
                {
                    dbPhysicalItem.passport_number = client.PassportNumber;
                    dbPhysicalItem.date_of_birth = client.DateOfBirth;
                    dbPhysicalItem.record_state = 2;
                    dbPhysicalItem.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }

                if (dbClient != null)
                {
                    dbClient.name = client.Name;
                    dbClient.record_state = 2;
                    dbClient.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }

                if (dbAddress != null)
                {
                    dbAddress.country = client.Address.Country;
                    dbAddress.city = client.Address.City;
                    dbAddress.street = client.Address.Street;
                    dbAddress.home_number = client.Address.HomeNumber;
                    dbAddress.record_state = 2;
                    dbAddress.record_updated = DateTime.UtcNow;

                    context.SaveChanges();
                }
            }
        }
    }
}
