namespace Dal.Entities;

internal class ProfilEntity
{
    public int ProfilId { get; set; }

    public string ProfilLbl { get; set; } = string.Empty;

    public bool DecouvertAutorise { get; set; } = false;
}