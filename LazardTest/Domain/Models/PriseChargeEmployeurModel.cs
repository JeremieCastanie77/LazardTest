namespace Domain.Models;

public class PriseChargeEmployeurModel(int priseChargeEmployeurId, int profilId, decimal priseCharge)
{
    public int PriseChargeEmployeurId { get; set; } = priseChargeEmployeurId;
    public int ProfilId { get; set; } = profilId;
    public decimal PriseCharge { get; set; } = priseCharge; 
}