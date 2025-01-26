using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Domain.Services;

public class ClientService(
    IClientRepository _clientRepository,
    ISupplementRepository _supplementRepository, 
    IPriseChargeEmployeurRepository _priseChargeEmployeurRepository,
    IProfilRepository _profilRepository) : IClientService
{
    public async Task<ClientModel> CrediteCompteAsync(int clientId, decimal montant)
    {
        var clientAcrediter = await _clientRepository.GetClientAsync(clientId) ?? throw new ClientNotFoundException();
        clientAcrediter.Compte += montant;

        var res = await _clientRepository.UpdateClientAsync(clientAcrediter);
        return res;
    }

    public async Task<TicketModel> PayerRepasAsync(int clientId, List<int> supplementsRepasId)
    {
        var clientPayant = await _clientRepository.GetClientAsync(clientId) ?? throw new ClientNotFoundException();
        var total = Constantes.PrixBaseRepas;

        var supplements = (await _supplementRepository.GetSupplementsAsync())?.ToList();

        var res = new TicketModel();

        foreach (var supplementId in supplementsRepasId)
        {
            var supplement = supplements?.FirstOrDefault(s => s.SupplementId == supplementId) ?? throw new SupplementNotFoundException();
            total += supplement.Prix;
            res.Supplements.Add(supplement);
        }

        var priseChargeEmployeurEntity = await _priseChargeEmployeurRepository.GetPriseChargeEmployeurByProfilAsync(clientPayant.ProfilId)
            ?? throw new PriseChargeEmployeurNotFoundException();
        if (priseChargeEmployeurEntity is not null)
            total = Math.Max(0, total - priseChargeEmployeurEntity.PriseCharge);

        if (clientPayant.Compte < total)
        {
            var profilEntity = await _profilRepository.GetProfilAsync(clientPayant.ProfilId) ?? throw new ProfilNotFoundException();
            if (!profilEntity.DecouvertAutorise) throw new ClientDecouvertNonAutoriseException();
        }

        clientPayant.Compte -= total;

        res.Total = total;

        await _clientRepository.UpdateClientAsync(clientPayant);

        return res;
    }
}