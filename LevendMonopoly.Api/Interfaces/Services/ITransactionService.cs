using LevendMonopoly.Api.Models;

namespace LevendMonopoly.Api.Interfaces.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> getTransactionsOfTeam(Guid teamId, int page);
        void AddTransaction(Transaction transaction);
    }
}
