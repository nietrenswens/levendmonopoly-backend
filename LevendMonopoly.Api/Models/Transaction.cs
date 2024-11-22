using LevendMonopoly.Api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LevendMonopoly.Api.Models
{
    public class Transaction : IHasID
    {
        private int amount;

        public Guid Id { get; set; } = new Guid();
        public Guid? Sender { get; init; }
        public Guid? Receiver { get; init; }
        public required int Amount
        {
            get
            {
                return amount;
            }
            init
            {
                if (amount < 0)
                    throw new ValidationException("Amount in transaction cannot be smaller than 0");
                amount = value;
            }
        }
        public required DateTime DateTime { get; init; }
        public required string Message { get; init; }
    }
}
