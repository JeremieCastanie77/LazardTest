using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Domain.Interfaces.Services;
using API.Requests;
using Domain.Exceptions;
using API.Response;

namespace Api.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    /// <summary>
    /// Crédite le compte d'un client et renvoie le client crédité avec le nouveau compte
    /// </summary>
    /// <param name="clientId">Identifiant du client</param>
    /// <param name="request">Contient la somme à créditer</param>
    /// <returns>Updated client</returns>
    [HttpPost("credite-compte/{clientId}")]
    public async Task<IActionResult> CrediteCompteAsync([FromRoute] int clientId, [FromBody] CrediteCompteRequest request)
    {
        var client = await clientService.CrediteCompteAsync(clientId, request.Montant);
        return Ok(new ClientDto(client));
    }

    /// <summary>
    /// Soustrait le prix d'un repas du compte d'un client et renvoie le ticket (détail des suppléments et total du ticket)
    /// </summary>
    /// <param name="clientId">Identifiant du client</param>
    /// <param name="request">Contient la liste des identifiants des éventuels suppléments du repas</param>
    /// <returns>Updated client</returns>
    [HttpPost("payer-repas/{clientId}")]
    public async Task<IActionResult> PayerRepasAsync([FromRoute] int clientId, [FromBody] PayerRepasRequest request)
    {
        try
        {
            var ticket = await clientService.PayerRepasAsync(clientId, request.SupplementsRepasId);
            return Ok(new PayerRepasResponse(ticket));
        }
        catch (Exception ex) when (ex is ClientDecouvertNonAutoriseException)
        {
            return BadRequest(ex.Message);
        }
    }
}