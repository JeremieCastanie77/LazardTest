using Dal.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Dal.Repositories;

public class ProfilRepository : IProfilRepository
{
    private static readonly IList<ProfilEntity> FakeProfilEntities =
    [
        new ProfilEntity
        {
            ProfilId = 1,
            ProfilLbl = "Interne",
            DecouvertAutorise = true
        },
        new ProfilEntity
        {
            ProfilId = 2,
            ProfilLbl = "Prestataire"
        },
        new ProfilEntity
        {
            ProfilId = 3,
            ProfilLbl = "VIP",
            DecouvertAutorise = true
        },
        new ProfilEntity
        {
            ProfilId = 4,
            ProfilLbl = "Stagiaire"
        },
        new ProfilEntity
        {
            ProfilId = 5,
            ProfilLbl = "Visiteur"
        }
    ];

    public async Task<ProfilModel> GetProfilAsync(int profilId)
    {
        var res = await Task.Run(() =>
        {
            var profilEntity = FakeProfilEntities.SingleOrDefault(p => p.ProfilId == profilId) ?? throw new ProfilNotFoundException();
            return profilEntity;
        });

        return new ProfilModel(res.ProfilId, res.ProfilLbl, res.DecouvertAutorise);
    }
}