using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Type { get; set; } = string.Empty;

        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}