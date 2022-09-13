using System;
using System.Collections.Generic;

namespace webmvcEF.DTO
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string? AccountNumber { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? BankName { get; set; }
        public string? Swiftcode { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
