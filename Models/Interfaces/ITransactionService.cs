using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<Transaction> GetTransaction(Guid id);
        Task<Guid> SaveTransaction(Transaction financialTransaction);
        Task<decimal> GetAccountBalance();
        Task<bool> IsValidTransaction(Transaction financialTransaction);
    }
}
