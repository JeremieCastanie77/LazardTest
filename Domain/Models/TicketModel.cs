namespace Domain.Models
{
    public class TicketModel
    {
        public List<SupplementModel> Supplements { get; set; } = [];

        public decimal Total = 0;
    }
}
