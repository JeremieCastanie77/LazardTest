namespace Domain.Exceptions;

public sealed class ProfilNotFoundException : Exception
{
    public ProfilNotFoundException() : base("Problème de données: profil non trouvé") { Source = Constantes.Prevu; }
}
