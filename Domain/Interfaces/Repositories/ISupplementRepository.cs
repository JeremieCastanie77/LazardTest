using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface ISupplementRepository
{
    Task<IEnumerable<SupplementModel>> GetSupplementsAsync();
}