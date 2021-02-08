using Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models.Interfaces;
using Models.Enums;

namespace Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly AppDbContext _db;
        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get transaction list from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> Get()
        {
            return await _db.Transactions.ToListAsync();
        }

        /// <summary>
        /// Get transaction by id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Transaction> Get(Guid id)
        {
            return await _db.Transactions.FirstOrDefaultAsync(n => n.Id == id);
        }

        /// <summary>
        /// Save transaction in the database
        /// </summary>
        /// <param name="financialTransaction"></param>
        /// <returns></returns>
        public async Task<Guid> Save(Transaction financialTransaction)
        {
            try
            {
                _db.Transactions.Add(financialTransaction);
                await _db.SaveChangesAsync();
                return financialTransaction.Id;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get account balance from the database
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetAccountBalance()
        {
            return await _db.Transactions.SumAsync(x => x.Type == TransactionType.Credit ? x.Amount : -x.Amount);
        }
    }    
}
