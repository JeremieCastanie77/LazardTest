namespace Domain.Models;

public class ClientModel(int clientId, string nom, string prenom, int profilId, decimal compte)
{
    public int ClientId { get; set; } = clientId;
    public string Nom { get; set; } = nom;
    public string Prenom { get; set; } = prenom;
    public int ProfilId { get; set; } = profilId;
    public decimal Compte { get; set; } = compte;
}