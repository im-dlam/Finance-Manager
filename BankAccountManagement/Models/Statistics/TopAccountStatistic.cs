using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models.Statistics
{
    public class TopAccountStatistic
    {
        public int Rank { get; set; }

        public string AccountNumber { get; set; }

        public string AccountHolder { get; set; }

        public decimal Balance { get; set; }

        public string AccountCurrency { get; set; }

        public decimal BalanceVND { get; set; }
    }
}