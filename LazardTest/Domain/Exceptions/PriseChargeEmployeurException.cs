namespace Domain.Exceptions;

public sealed class PriseChargeEmployeurNotFoundException : Exception
{
    public PriseChargeEmployeurNotFoundException() : base("Problème de données: prise en charge non trouvée") { Source = Constantes.Prevu; }
}
