using System;

namespace BankClassLibrary
{
    public class CurrentAccountPlus : Bank, IPremiumAccount
    {
        private decimal customOverdraftLimit;

        public CurrentAccountPlus(string bankName, string accountHolder, decimal balance, decimal overdraftLimit)
            : base(bankName, accountHolder, balance)
        {
            if (overdraftLimit > 0 || overdraftLimit < -10000)
            {
                throw new ArgumentException("Overdraft limit must be between 0 and -10,000.");
            }

            this.customOverdraftLimit = overdraftLimit;
            base.SetOverdraftLimit(overdraftLimit);
        }

        public decimal CustomOverdraftLimit => customOverdraftLimit;



        public override void Withdraw(decimal amount)
        {
            const decimal transactionFee = 5.00m;

            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            decimal totalAmount = amount + transactionFee;

            if (this.Balance - totalAmount < this.customOverdraftLimit)
                throw new InvalidOperationException($"Insufficient funds. Cannot exceed custom overdraft limit of {customOverdraftLimit:C} including transaction fee.");

            this.Balance -= totalAmount;
            Console.WriteLine($"£{transactionFee} transaction fee applied. Withdrawn: £{amount}, Total deducted: £{totalAmount}");
        }
    }
}