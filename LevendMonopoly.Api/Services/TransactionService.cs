using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LevendMonopoly.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.Sender == null && transaction.Receiver == null)
                throw new ValidationException("Both transaction sender and receiver were null.");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> getTransactionsOfTeam(Guid teamId, int page)
        {
            int indx = page - 1;
            if (indx < 0) throw new ValidationException("Page cannot be smaller than 1");

            return _context.Transactions.OrderByDescending(transaction => transaction.DateTime)
                .Where(transaction => transaction.Receiver == teamId || transaction.Sender == teamId)
                .Take(15).Skip(indx * 15).ToList();
        }

        public void ResetTransactions()
        {
            _context.Transactions.Clear();
            _context.SaveChanges();
        }
    }
}
