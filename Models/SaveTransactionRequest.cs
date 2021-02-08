using Models.Enums;

namespace Models
{
    public class SaveTransactionRequest
    {
        public TransactionType Type { get; set; }        
        public decimal Amount { get; set; }        
    }
}
