namespace Domain.Models;

public class ProfilModel
{
    public int ProfilId { get; set; }
    public string ProfilLbl { get; set; } = string.Empty;
    public bool DecouvertAutorise { get; set; } = false;

    public ProfilModel(int profilId, string profilLbl, bool decouvertAutorise)
    {
        ProfilId = profilId;
        ProfilLbl = profilLbl;
        DecouvertAutorise = decouvertAutorise;
    }

    public ProfilModel(int profilId, string profilLbl)
    {
        ProfilId = profilId;
        ProfilLbl = profilLbl;
    }
}