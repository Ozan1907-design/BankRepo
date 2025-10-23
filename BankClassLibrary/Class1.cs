using System.Data;

namespace BankClassLibrary
{
    public class Bank
    {
        private string bankName;
        private string accountnumber;
        private string accountHolder;
        private decimal balance;
        private string sortcode;
        private decimal overdraftLimit = -1000;
        public static int accountCount { get; private set; } = 0;

        private static List<int> existingAccountNumbers = new List<int>();
        private static List<int> existingSortCodes = new List<int>();

        public Bank(string bankName, string accountHolder, decimal balance)
        {
            Random random = new Random();
            int randomAccountNumber = random.Next(10000000, 100000000);
            int randomSortCode = random.Next(100000, 1000000);
            while (existingAccountNumbers.Contains(randomAccountNumber) || (existingSortCodes.Contains(randomSortCode)))
            {
                randomAccountNumber = random.Next(10000000, 100000000);
                randomSortCode = random.Next(100000, 1000000);
            }

            this.BankName = bankName;
            this.AccountNumber = randomAccountNumber.ToString();
            this.AccountHolder = accountHolder;
            this.Balance = balance;
            this.SortCode = randomSortCode.ToString();
            accountCount++;
        }

        List<string> banknames = new List<string>()
        {
            "Bank of America",
            "Chase Bank",
            "Wells Fargo",
            "Citibank",
            "Nationwide",
            "Natwest"
        };
        public string BankName
        {
            get { return this.bankName; }
            set
            {
                if (!banknames.Contains(value))
                {
                    throw new ArgumentException("Invalid bank name.");
                }
                this.bankName = value;

            }
        }

        public string AccountNumber
        {
            get { return accountnumber; }
            private set
            {
                if (value.Length != 8 || !value.All(char.IsDigit))
                {
                    throw new ArgumentException("Account number must be exactly 8 digits.");
                }
                this.accountnumber = value;
            }
        }

        public string AccountHolder
        {
            get { return this.accountHolder; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Account holder name cannot be empty.");
                }
                this.accountHolder = value;
            }
        }

        public decimal Balance
        {
            get { return this.balance; }
            set
            {
                if (value < this.overdraftLimit)
                {
                    throw new ArgumentException("Our credit limit is -1000");
                }
                this.balance = value;
            }
        }

        public string SortCode
        {
            get { return this.sortcode; }
            private set
            {
                if (value.Length != 6 || !value.All(char.IsDigit))
                {
                    throw new ArgumentException("Sort code must be exactly 6 digits.");
                }
                this.sortcode = value;
            }
        }


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            this.Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }
            if (this.Balance - amount < this.overdraftLimit)
            {
                throw new InvalidOperationException("Insufficient funds. Cannot exceed credit limit of -1000.");
            }
            this.Balance -= amount;
        }

        public void Transfer(Bank targetAccount, string sortcode, decimal amount)
        {
            if (targetAccount == null)
            {
                throw new ArgumentNullException(nameof(targetAccount), "Target account cannot be null.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be positive.");
            }
            if (this.Balance - amount < this.overdraftLimit)
            {
                throw new InvalidOperationException("Insufficient funds. Cannot exceed credit limit of -1000.");
            }
            if (int.Parse(targetAccount.SortCode) != int.Parse(sortcode))
            {
                throw new ArgumentException("Sortcode and Account number dont match");
            }
            this.Withdraw(amount);
            targetAccount.Deposit(amount);
        }

        public void SetOverdraftLimit(decimal limit)
        {
            if (limit > 0 || limit < -10000)
            {
                throw new ArgumentException("Overdraft limit must be between 0 and -10,000.");
            }
            this.overdraftLimit = limit;
        }

        public decimal GetOverdraftlimit()
        {
            return this.overdraftLimit;
        }
    }
}
