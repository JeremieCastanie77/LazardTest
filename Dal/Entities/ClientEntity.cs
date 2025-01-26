namespace Dal.Entities;

internal class ClientEntity
{
    public int ClientId { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public int ProfilId { get; set; } = 0;

    public decimal Compte { get; set; } = 0;

    public ProfilEntity Profil { get; set; } = null!;
}