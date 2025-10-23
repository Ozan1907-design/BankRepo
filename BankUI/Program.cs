using BankClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static List<Bank> bankAccounts = new List<Bank>();


    static void Main()
    {
        bool exit = false;
        while (exit == false)
        {
            Console.WriteLine("""

    Welcome to the Bank Account Management System
    Please select an option:
    1. Create a new bank account
    2. Deposit funds
    3. Withdraw funds
    4. Transfer funds
    5. View account details
    6. Exit
    """);
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    DepositFunds();
                    break;
                case "3":
                    WithdrawFunds();
                    break;
                case "4":
                    TransferFunds();
                    break;
                case "5":
                    ViewAccount();
                    break;
                case "6":
                    exit = true;
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                case "7":
                    CreatePremiumAccount();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }
    public static void CreateAccount()
    {
        Console.Write("Enter Bank Name: ");
        string bankName = Console.ReadLine();
        Console.Write("Enter Account Holder Name: ");
        string accountHolder = Console.ReadLine();
        Console.Write("Enter Initial Deposit Amount: ");
        string initialDepositStr = Console.ReadLine();
        decimal initialDeposit;
        while (!decimal.TryParse(initialDepositStr, out initialDeposit) || initialDeposit < 0)
        {
            Console.Write("Invalid amount. Please enter a valid Initial Deposit Amount: ");
            initialDepositStr = Console.ReadLine();
        }

        try
        {
            Bank newAccount = new Bank(bankName, accountHolder, initialDeposit);
            bankAccounts.Add(newAccount);
            Console.WriteLine($"Bank account created successfully! Account Number: {newAccount.AccountNumber}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error creating account: {ex.Message}");
        }

    }
    static void CreatePremiumAccount()
    {
        Console.Write("Enter Bank Name: ");
        string bankName = Console.ReadLine();
        Console.Write("Enter Account Holder Name: ");
        string accountHolder = Console.ReadLine();
        Console.Write("Enter Initial Deposit Amount: ");
        decimal initialDeposit = decimal.Parse(Console.ReadLine());
        Console.Write("Enter Custom Overdraft Limit (e.g., -5000): ");
        decimal overdraftLimit = decimal.Parse(Console.ReadLine());

        try
        {
            CurrentAccountPlus premium = new CurrentAccountPlus(bankName, accountHolder, initialDeposit, overdraftLimit);
            bankAccounts.Add(premium);
            Console.WriteLine($"Premium account created. Account Number: {premium.AccountNumber}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    public static Bank FindAccountByNumber(string accountNumber)
    {
        return bankAccounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
    }
    public static void DepositFunds()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Bank account = FindAccountByNumber(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }
        Console.Write("Enter Deposit Amount: ");
        string depositAmountStr = Console.ReadLine();
        decimal depositAmount;
        while (!decimal.TryParse(depositAmountStr, out depositAmount) || depositAmount <= 0)
        {
            Console.Write("Invalid amount. Please enter a valid Deposit Amount: ");
            depositAmountStr = Console.ReadLine();
        }
        try
        {
            account.Deposit(depositAmount);
            Console.WriteLine($"Deposit successful! New Balance: {account.Balance}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error during deposit: {ex.Message}");
        }
    }

    public static void WithdrawFunds()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Bank account = FindAccountByNumber(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }
        Console.Write("Enter Withdrawal Amount: ");
        string withdrawalAmountStr = Console.ReadLine();
        decimal withdrawalAmount;
        while (!decimal.TryParse(withdrawalAmountStr, out withdrawalAmount) || withdrawalAmount <= 0)
        {
            Console.Write("Invalid amount. Please enter a valid Withdrawal Amount: ");
            withdrawalAmountStr = Console.ReadLine();
        }
        try
        {
            if (account is IPremiumAccount premium)
            {
                premium.Withdraw(withdrawalAmount);
            }
            else
            {
                account.Withdraw(withdrawalAmount);
            }

            Console.WriteLine($"Withdrawal successful! New Balance: {account.Balance}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error during withdrawal: {ex.Message}");
        }
    }

    public static void TransferFunds()
    {
        Console.Write("Enter Your Account Number: ");
        string fromAccountNumber = Console.ReadLine();
        Bank fromAccount = FindAccountByNumber(fromAccountNumber);
        if (fromAccount == null)
        {
            Console.WriteLine("Your account not found.");
            return;
        }
        Console.Write("Enter Target Account Number: ");
        string toAccountNumber = Console.ReadLine();
        Bank toAccount = FindAccountByNumber(toAccountNumber);
        if (toAccount == null)
        {
            Console.WriteLine("Target account not found.");
            return;
        }
        Console.Write("Enter Transfer Amount: ");
        string transferAmountStr = Console.ReadLine();
        Console.Write("Enter Target Account Sortcode");
        string sortcode = Console.ReadLine();
        decimal transferAmount;
        while (!decimal.TryParse(transferAmountStr, out transferAmount) || transferAmount <= 0)
        {
            Console.Write("Invalid amount. Please enter a valid Transfer Amount: ");
            transferAmountStr = Console.ReadLine();
        }
        try
        {
            fromAccount.Transfer(toAccount, sortcode , transferAmount);
            Console.WriteLine($"Transfer successful! Your New Balance: {fromAccount.Balance}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error during transfer: {ex.Message}");
        }
    }

    public static void ViewAccount()
    {
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Bank account = FindAccountByNumber(accountNumber);
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }
        Console.WriteLine($@"
        Account Details:
        {account.AccountHolder}'s Account
        Bank Name: {account.BankName}
        Account Number: {account.AccountNumber}
        Sort Code: {account.SortCode}
        Balance: {account.Balance:C}
        Overdraft Limit: {account.GetOverdraftlimit()}
        ");


    }
}






