﻿using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// Financial transaction model
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Unique transaction identifier, generated by the service, when the transaction is being stored.
        /// </summary>
        public Guid Id { get; set; }        
        public TransactionType Type { get; set; }
        /// <summary>
        /// Transaction numeric value. Incrementing or decrementing the account balance, based on the transaction type.
        /// </summary>
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Effective date-time, generated by the service, when the transaction is being stored.
        /// </summary>
        public DateTime EffectiveDate { get; set; }        
        [Timestamp]
        public byte[] RowVersion { get; set; }        
    }
}
