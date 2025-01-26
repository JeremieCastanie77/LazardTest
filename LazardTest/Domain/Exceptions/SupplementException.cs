namespace Domain.Exceptions;

public sealed class SupplementNotFoundException : Exception
{
    public SupplementNotFoundException() : base("Problème de données: supplément non trouvé") { Source = Constantes.Prevu; }
}
