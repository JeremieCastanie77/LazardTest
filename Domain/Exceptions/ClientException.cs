namespace Domain.Exceptions;

public sealed class ClientNotFoundException : Exception
{
    public ClientNotFoundException() : base("Problème de données: client non trouvé") { Source = Constantes.Prevu; }
}

public sealed class ClientDecouvertNonAutoriseException : Exception
{
    public ClientDecouvertNonAutoriseException() : base("Le client n'est pas autorisé à avoir un découvert.") { Source = Constantes.Prevu; }
}