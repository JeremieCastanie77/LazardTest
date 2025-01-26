using Dal.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Dal.Repositories;

public class PriseChargeEmployeurRepository : IPriseChargeEmployeurRepository
{
    private static readonly IList<PriseChargeEmployeurEntity> FakePriseChargeEmployeurEntities =
    [
        new PriseChargeEmployeurEntity
        {
            PriseChargeEmployeurEntityId = 1,
            ProfilId = 1,
            PriseCharge = 7.5m
        },
        new PriseChargeEmployeurEntity
        {
            PriseChargeEmployeurEntityId = 2,
            ProfilId = 2,
            PriseCharge = 6
        },
        new PriseChargeEmployeurEntity
        {
            PriseChargeEmployeurEntityId = 3,
            ProfilId = 3,
            PriseCharge = decimal.MaxValue
        },
        new PriseChargeEmployeurEntity
        {
            PriseChargeEmployeurEntityId = 4,
            ProfilId = 4,
            PriseCharge = 10
        }
    ];

    public async Task<PriseChargeEmployeurModel> GetPriseChargeEmployeurByProfilAsync(int profilId)
    {
        var res = await Task.Run(() =>
        {
            var priseChargeEmployeurEntity = FakePriseChargeEmployeurEntities.SingleOrDefault(pce => pce.ProfilId == profilId)
                ?? throw new PriseChargeEmployeurNotFoundException();
            return priseChargeEmployeurEntity;
        });

        return new PriseChargeEmployeurModel(res.PriseChargeEmployeurEntityId, res.ProfilId, res.PriseCharge);
    }
}