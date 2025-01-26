namespace Domain.Models;

public class SupplementModel(int supplementId, string supplementLbl, decimal prix)
{
    public int SupplementId { get; set; } = supplementId;
    public string SupplementLbl { get; set; } = supplementLbl;
    public decimal Prix { get; set; } = prix;
}