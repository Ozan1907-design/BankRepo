using BankClassLibrary;

Bank bank1 = new Bank("Nationwide", "12345678", "John Doe", 1000.00m, "123456");
Bank bank2 = new Bank("Natwest", "87654321", "Jane Smith", 2500.50m, "654321");

Console.WriteLine($"Bank 1 Details: {bank1.BankName}, {bank1.SortCode}");
Console.WriteLine($"Bank 2 Details: {bank2.BankName}, {bank2.SortCode}");

Console.WriteLine("Depositing 500.00 to Bank 1");
bank1.Deposit(500.00m);
Console.WriteLine("Deposited 500.00 to Bank 1 \n Bank 1 Balance is now :");
Console.WriteLine(bank1.Balance);

Console.WriteLine("transferring 300.00 from Bank 1 to Bank 2");
bank2.Transfer(bank1, 300.00m);
Console.WriteLine($"Bank 1 new balance: {bank1.Balance} , Bank 2 new Balance: {bank2.Balance}");




