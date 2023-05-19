using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Bank_Model;
using System.IO;


namespace BankAppNUnitTest
{
    [TestFixture]
    internal class NUnitValidatiionClass
    {

        [Test]
        [TestCase("3000", 0, ExpectedResult = true)]
        [TestCase("ty55", 0, ExpectedResult = false)]
        public bool IsIntegerValidationTest(string s, ref int number)
        {
            return Validation.IsInteger(s, ref number);
        }

        #region IsDouble
        [Test]
        public void isDoubleValidationTest()
        {
            // Arrange
            double number = 0;
            string s = "123.45";

            // Act
            bool result = Validation.IsDouble(s, ref number);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(123.45, number);
        }

        [Test]
        public void TestIsDoubleInvalidInput()
        {
            // Arrange
            double number = 0;
            string s = "abc";

            // Act
            bool result = Validation.IsDouble(s, ref number);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region Transfer
        [Test]
        public void TestTransferValidationValidInput()
        {
            // Arrange
            string amount = "200";
            string balance = "20346";

            // Act
            bool result = Validation.TransferValidateReplica(amount, balance);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TestTransferValidationInsufficientFunds()
        {
            // Arrange
            string amount = "50000";
            string balance = "2000";

            // Act
            bool result = Validation.TransferValidateReplica(amount, balance);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion


        [Test]
        [TestCase("2011136798", ExpectedResult = true)]
        [TestCase("201567812a", ExpectedResult = false)]
        [TestCase("2015678", ExpectedResult = false)]
        public static bool SearchAccountValidation(string number)
        {
            return Validation.searchAccountValidation(number);
        }


        [Test]
        [TestCase("2011136798", "1000", ExpectedResult = true)]
        [TestCase("201567812a", "0", ExpectedResult = false)]
        [TestCase("2015678", "2000", ExpectedResult = false)]
        public bool DepositAccountValidation(string AcctNum, string amount)
        {
            return Validation.DepositValidation(AcctNum, amount);
        }


        [Test]
        [TestCase("steve", "jasper", ExpectedResult = false)]
        [TestCase("201567812a", "0", ExpectedResult = false)]
        //[TestCase("Steve", "Jasper", ExpectedResult = false)]
        public bool CapitalizeCheckTest(string Fname, string Lname)
        {
            return Validation.CapitalizeCheck(Fname, Lname);
        }

        [Test]
        public void depositAddToBalance_depositLimit()
        {
            Account account = new Account();
            account.Balance = 100;
            double amount = 50;
            double expected = 150;

            account.depositAddToBalance(amount);

            Assert.AreEqual(expected, account.Balance);
        }


        [Test]
        public void depositAddToBalance_depositLimitArgumentOutOfRangeException()
        {
            Account account = new();

            account.Balance = 100;
            double amount = 0;

            account.depositAddToBalance(amount);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.depositAddToBalance(amount));

        }



    }
}
