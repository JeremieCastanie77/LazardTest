namespace Dal.Entities;

internal class SupplementEntity
{
    public int SupplementId { get; set; }

    public string SupplementLbl { get; set; } = string.Empty;

    public decimal Prix { get; set; } = 0;
}