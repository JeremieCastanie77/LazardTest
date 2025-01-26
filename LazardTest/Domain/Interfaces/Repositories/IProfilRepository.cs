using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IProfilRepository
{
    Task<ProfilModel> GetProfilAsync(int profilId);
}