using BankClassLibrary;
namespace BankAccountUnitTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestCreationOfBank()
        {
            Bank bank = new Bank("Natwest", "11223344", "Alice Johnson", 500.00m, "112233");
            Assert.AreEqual("Natwest", bank.BankName);
        }

        [TestMethod]
        public void TestDepositMethod()
        {
            Bank bank = new Bank("Natwest", "11223344", "Alice Johnson", 500.00m, "112233");
            bank.Deposit(250.00m);
            Assert.AreEqual(750.00m, bank.Balance);
        }
    }
}
