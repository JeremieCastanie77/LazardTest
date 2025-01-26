using Domain.Models;

namespace Api.Dtos;

public record ClientDto(int ClientId, string Nom, string Prenom, int ProfilId, decimal Compte)
{
    public ClientDto(ClientModel client) : this(client.ClientId, client.Nom, client.Prenom, client.ProfilId, client.Compte) { }
}