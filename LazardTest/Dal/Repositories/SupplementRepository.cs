using Dal.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Dal.Repositories;

public class SupplementRepository : ISupplementRepository
{
    private static readonly IList<SupplementEntity> FakeSupplementEntities =
    [
        new SupplementEntity
        {
            SupplementId = 1,
            SupplementLbl = "Boisson",
            Prix = 1
        },
        new SupplementEntity
        {
            SupplementId = 2,
            SupplementLbl = "Fromage",
            Prix = 1
        },
        new SupplementEntity
        {
            SupplementId = 3,
            SupplementLbl = "Pain",
            Prix = 0.4m
        },
        new SupplementEntity
        {
            SupplementId = 4,
            SupplementLbl = "Petite salade bar",
            Prix = 4
        },
        new SupplementEntity
        {
            SupplementId = 5,
            SupplementLbl = "Grande salade bar",
            Prix = 6
        },
        new SupplementEntity
        {
            SupplementId = 6,
            SupplementLbl = "Portion de fruit",
            Prix = 1
        },
        new SupplementEntity
        {
            SupplementId = 7,
            SupplementLbl = "Entrée supplémentaire",
            Prix = 3
        },
        new SupplementEntity
        {
            SupplementId = 8,
            SupplementLbl = "Plat supplémentaire",
            Prix = 6
        },
        new SupplementEntity
        {
            SupplementId = 9,
            SupplementLbl = "Dessert supplémentaire",
            Prix = 3
        }
    ];

    public async Task<IEnumerable<SupplementModel>> GetSupplementsAsync()
    {
        var res = await Task.Run(() =>
        {
            var ret = 
            from s in FakeSupplementEntities
            select new SupplementModel(s.SupplementId, s.SupplementLbl, s.Prix);

            return ret;
        });

        if (res is null) return [];

        return res;
    }
}