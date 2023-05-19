using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Model
{
    public class Validation
    {
        public static bool acctNumValidate(List<string> Title)
        {
            //if (!File.Exists(string.Format("{0}", int.Parse(Title[0]))) && !File.Exists(string.Format("{0}", int.Parse(Title[1]))))
            //{
            //    Console.WriteLine("This Account Number does Not Exist");
            //    return false;
            //}
            return true;
        }

        #region TransferValidation
        public static bool TransferValidation(string amount, string accountNum) 
        { 
            Account check = new(int.Parse(accountNum));
            check.load();
            if (double.Parse(amount) > check.Balance)
            {
                Console.WriteLine("OOPS!!.... You do not have the amount you are trying to transfer");

                return false;
            }
            return true;
        }
        #endregion

        public static bool TransferValidateReplica(string amount, string balance)
        {
            if(int.Parse(amount) > int.Parse(balance))
            {
                return false;
            }
            return true;
        }


        public static bool CapitalizeCheck(string Fname, string Lname)
        {
            int check = 0;
            //if (string.IsNullOrEmpty(Fname)) { return false; }
            //if (string.IsNullOrEmpty(Lname)) { return false; }
            if (Fname[..1] == Fname[..1].ToLower() || Lname[..1] == Lname[..1].ToLower() || IsInteger(Fname, ref check ) || IsInteger(Lname, ref check))
            {
                Console.WriteLine("Name has to be a capitalized and without digits");
                
                return false;

            }
            return true;
        }

        public static bool DepositValidation(string acctNum, string amount )
        {
            int account = 0;
            if (!IsInteger(acctNum, ref account) || acctNum.Length > 10 || acctNum.Length < 10)
            {
                Console.WriteLine("Invalid account !");
                return false;
            }
            double amountsRef = 0;
            if (!IsDouble(amount, ref amountsRef))
            {
                Console.WriteLine("Invalid amount !");
                return false;
            }
            if (int.Parse(amount) <= 0)
            {
                Console.WriteLine("Invalid amount !");
                return false;
            }
            return true;
        }

        public static bool WithdrawLimitValidation(string accNum, string amounts)
        {
            int accountNumber = int.Parse(accNum);
            double amount = double.Parse(amounts);

            Account account = new Account(accountNumber);
            account.load();

            if (amount > account.Balance)
            {
                Console.WriteLine("Not enough money to withdraw in the account !");
                return false;
            }
            if (amount < 0)
            {
                Console.WriteLine("Sorry, You can not make withdrawals of $1000 and below");
                return false;
            }
            return true;
        }

        public static bool searchAccountValidation(string AccountNumber)
        {
            int account = 0;
            if (!IsInteger(AccountNumber, ref account) || AccountNumber.Length > 10 || AccountNumber.Length < 10)
            {
                Console.WriteLine("Invalid account !");
                return false;
            }
            return true;
        }
        public static bool IsInteger(string s, ref int number)
        {
            try
            {
                number = int.Parse(s);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool IsDouble(string s, ref double number)
        {
            try
            {
                number = double.Parse(s);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



    }
}
