using Api.Dtos;
using Domain.Models;

namespace API.Response
{
    public record PayerRepasResponse(IEnumerable<SupplementDto> Supplements, decimal Total)
    {
        public PayerRepasResponse(TicketModel ticket) : this(ticket.Supplements.Select(x => new SupplementDto(x.SupplementLbl, x.Prix)), ticket.Total) { }
    }
}