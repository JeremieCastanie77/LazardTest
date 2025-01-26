using Dal.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Dal.Repositories;

public class ClientRepository : IClientRepository
{
    private static readonly IList<ClientEntity> FakeClientEntities =
    [
        new ClientEntity
        {
            ClientId = 100,
            Nom = "Fake nom 100",
            Prenom = "Fake prenom 100",
            ProfilId = 1,
            Compte = 25.6m
        },
        new ClientEntity
        {
            ClientId = 110,
            Nom = "Fake nom 110",
            Prenom = "Fake prenom 110",
            ProfilId = 2,
            Compte = 44.5m
        }
    ];

    public async Task<ClientModel> GetClientAsync(int clientId)
    {
        var res = await Task.Run(() =>
        {
            var clientEntity = FakeClientEntities.SingleOrDefault(c => c.ClientId == clientId) ?? throw new ClientNotFoundException();
            return clientEntity;
        });

        return new ClientModel(res.ClientId, res.Nom, res.Prenom, res.ProfilId, res.Compte);
    }

    // Comme c'est une fausse bdd, on ne fait que renvoyer le clientModel après avoir vérifié qu'il existe en base
    public async Task<ClientModel> UpdateClientAsync(ClientModel clientModel)
    {
        var res = await Task.Run(() =>
        {
            var clientEntity = FakeClientEntities.SingleOrDefault(c => c.ClientId == clientModel.ClientId) ?? throw new ClientNotFoundException();
            return clientEntity;
        });

        return clientModel;
    }
}