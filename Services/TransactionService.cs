using Microsoft.Extensions.Caching.Memory;
using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _financialTransactionRepository;
        private readonly IMemoryCache _cache;
        public TransactionService(ITransactionRepository financialTransactionRepository, IMemoryCache memoryCache)
        {
            _financialTransactionRepository = financialTransactionRepository;
            _cache = memoryCache;
        }

        #region public methods
        /// <summary>
        /// Get transaction history
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            List<Transaction> cacheEntry;
            if (!_cache.TryGetValue($"TransactionHistory", out cacheEntry))
            {
                cacheEntry = await _financialTransactionRepository.Get();
            }            
            return cacheEntry;
        }

        /// <summary>
        /// Get transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await _financialTransactionRepository.Get(id);
        }

        /// <summary>
        /// Save transaction
        /// </summary>
        /// <param name="financialTransaction"></param>
        /// <returns>Transaction id</returns>
        public async Task<Guid> SaveTransaction(Transaction financialTransaction)
        {            
            var newTransaction = await _financialTransactionRepository.Save(financialTransaction);
            var transactionHistory = await _financialTransactionRepository.Get();
            var cacheEntryOptions = new MemoryCacheEntryOptions();            
            _cache.Set($"TransactionHistory", transactionHistory, cacheEntryOptions);            
            return newTransaction;
        }

        /// <summary>
        /// Get account balance
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetAccountBalance()
        {
            return await _financialTransactionRepository.GetAccountBalance();
        }

        /// <summary>
        /// Validate transaction 
        /// </summary>
        /// <param name="financialTransaction"></param>
        /// <returns></returns>
        public async Task<bool> IsValidTransaction(Transaction financialTransaction)
        {
            if (financialTransaction.Amount <= 0)
            {
                return false;
            }
            var balance = await _financialTransactionRepository.GetAccountBalance();
            
            if(financialTransaction.Type == Models.Enums.TransactionType.Credit)               
                return balance + financialTransaction.Amount > 0;
            else
                return balance - financialTransaction.Amount > 0;
        }
        #endregion

        #region private methods
        #endregion
    }
}
