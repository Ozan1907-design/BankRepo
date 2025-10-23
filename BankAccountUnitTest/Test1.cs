using BankClassLibrary;
namespace BankAccountUnitTest
{
    [TestClass]
    public sealed class Test1
    {
     [TestMethod]
        public void TestCreateBankAccount()
            {
                
                string bankName = "Chase Bank";
                string accountHolder = "Ozan Bas";
                decimal initialBalance = 500.00m;
    
               
                Bank newAccount = new Bank(bankName, accountHolder, initialBalance);
    
              
                Assert.AreEqual(bankName, newAccount.BankName);
                Assert.AreEqual(accountHolder, newAccount.AccountHolder);
                Assert.AreEqual(initialBalance, newAccount.Balance);
                Assert.IsNotNull(newAccount.AccountNumber);
                Assert.IsNotNull(newAccount.SortCode);
        }
    }
}
