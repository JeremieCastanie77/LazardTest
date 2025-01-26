namespace Dal.Entities;

internal class PriseChargeEmployeurEntity
{
    public int PriseChargeEmployeurEntityId { get; set; }

    public int ProfilId { get; set; }

    public decimal PriseCharge { get; set; } = 0;

    public ProfilEntity Profil { get; set; } = null!;
}