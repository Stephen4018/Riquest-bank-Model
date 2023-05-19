using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Model
{
    internal class Menu
    {
        public static void AccountMenu()
        {
            string userChoice;
            int result;

            do
            {
                Console.Write(@"PRESS 1 ====> CREATE ACCOUNT
            PRESS 2 ====> DEPOSIT
            PRESS 3 ====> WITHDRAW
            PRESS 4 ====> TRANSFER
            PRESS 5 ====> SEARCH
        ");

                userChoice = Console.ReadLine();
                if (userChoice == "1")
                {
                    Bank.CreateAccount();
                }
                else if (userChoice == "2")
                {
                    Bank.Deposit();
                }
                else if (userChoice == "3")
                {
                    Bank.Withdraw();
                }
                else if (userChoice == "4")
                {
                    Bank.Transfer();
                }
                else if (userChoice == "5")
                {
                    Bank.SearchAccount();
                }
            } while (int.Parse(userChoice) > 5 || !int.TryParse(userChoice, out result));
       
        }
    }
}
