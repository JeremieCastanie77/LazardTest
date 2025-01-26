using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IClientService
{
    Task<ClientModel> CrediteCompteAsync(int clientId, decimal montant);

    Task<TicketModel> PayerRepasAsync(int clientId, List<int> supplementsRepasId);
}