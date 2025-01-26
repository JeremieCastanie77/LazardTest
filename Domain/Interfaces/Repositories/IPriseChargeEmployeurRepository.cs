using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IPriseChargeEmployeurRepository
{
    Task<PriseChargeEmployeurModel> GetPriseChargeEmployeurByProfilAsync(int profilId);
}