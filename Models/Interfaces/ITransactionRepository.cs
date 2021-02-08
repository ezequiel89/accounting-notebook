using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> Get();
        Task<Transaction> Get(Guid id);
        Task<Guid> Save(Transaction financialTransaction);
        Task<decimal> GetAccountBalance();
    }
}
