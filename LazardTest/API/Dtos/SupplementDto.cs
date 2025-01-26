using Domain.Models;

namespace Api.Dtos;

public record SupplementDto(string SupplementLbl, decimal Prix)
{
    public SupplementDto(SupplementModel supplement) : this(supplement.SupplementLbl, supplement.Prix) { }
}