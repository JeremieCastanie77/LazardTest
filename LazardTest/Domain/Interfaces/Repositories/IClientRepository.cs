using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IClientRepository
{
    Task<ClientModel> GetClientAsync(int clientId);

    Task<ClientModel> UpdateClientAsync(ClientModel client);
}