using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using ConsoleTables;


namespace Bank_Model
{


    public class Bank
    {
        public static void CreateAccount()
        {

            List<string> fieldTitles = new List<string>() { "First Name: ", "Last Name: ", "AccountType: ", "Initial Deposit: " };

            List<string> fields;
            do
            {
                fields = HelperClass.GetInfo(fieldTitles);
            }
            while (!Validation.CapitalizeCheck(fields[0], fields[1]));


            Console.Write("Is the information correct (y/n): ");
            string confirm = Console.ReadLine().ToLower();
            if (confirm == "y")
            {

                Account account = new Account(HelperClass.GenerateAccount(fields[2]), 0,
                    fields[0], fields[1], fields[2]);
                account.depositAddToBalance(int.Parse(fields[3]));
                account.save();
                account.load();

                #region Table
                string concaten = account.FirstName + " " + account.LastName;
                var table = new ConsoleTable("FUllNAME", "ACCOUNT NUMBERR", "ACCOUNT TYPE", "BALANCE");
                table.AddRow(concaten, account.AccountNumber, account.AccountType, account.Balance);
                table.Write();
                #endregion


                Exit_Repeat.Confirm();
            }
        }


        public static void Transfer()
        {
            List<string> fieldTitles = new List<string>() { "Recipient Account Number: ", "Senders Account Number: ", "Amount: $", "Note: " };
            List<string> fields;
            bool permit;
            do
            {
                fields = HelperClass.GetInfo(fieldTitles);
            }
            while (!Validation.acctNumValidate(fields));

            Console.WriteLine("Account Found");
           
            permit = Validation.TransferValidation(fields[2], fields[1]);

            if (permit)
            {

                #region recipient
                int recipient = int.Parse(fields[0]); //THIS IS TO GET THE RECIPEINTS ACCOUNT DETAILS
                Account account = new(recipient);
                account.load();
                account.depositAddToBalance(double.Parse(fields[2]));
                account.save();
                Console.WriteLine("ALERT::::::  Your Account {0} has been credited with ${1}", account.AccountNumber, fields[2]);
                #endregion

                #region sender
                int sender = int.Parse(fields[1]);
                Account accounts = new(sender);
                accounts.load();
                accounts.withdrawSubtractFromBalance(double.Parse(fields[2]));
                accounts.save();
                Console.WriteLine("Your Account {0} has been debited with ${1}", accounts.AccountNumber, fields[2]);
                #endregion

                #region RECIEPT
                Console.WriteLine("THIS IS YOUR RECIEPT");
                string concaten = account.FirstName + " " + account.LastName;
                var table = new ConsoleTable("RECIPIENT FUllNAME", concaten);
                table.AddRow("RECIPIENT ACCT NUM", fields[0].Substring(0, 4) + "****" + fields[0].Substring(8));
                table.AddRow("AMOUNT TRANSFERED", $"${fields[2]}");
                table.AddRow("BALANCE", $"${accounts.Balance}");
                table.AddRow("NOTE", fields[3]);
                table.Write();
                #endregion
            }

            Exit_Repeat.Confirm();


        }

        public static void Deposit()
        {
            List<string> fieldTitles = new List<string>();
            List<string> fields = null;

            fieldTitles.Add("Account Number:");
            fieldTitles.Add("Amount: $");
            bool found = false;
            string confirm;
            while (!found)
            {
                do
                {
                    fields = HelperClass.GetInfo(fieldTitles);
                }
                while (!Validation.DepositValidation(fields[0], fields[1]));

                found = File.Exists(string.Format("{0}.txt", fields[0]));

                if (!found)
                {
                    Console.WriteLine("Account not found !");
                    Console.Write("Retry (y/n): ");
                    confirm = Console.ReadLine();
                    if (confirm == "y") continue;
                    else return;
                }
            }
            Console.WriteLine("Account found !");
            
            int accountNumber = int.Parse(fields[0]); //THIS CONVERTS THE ACCOUNT NUMBER TO A NUMBER
            Account account = new(accountNumber);
            account.load();
            account.depositAddToBalance(double.Parse(fields[1]));
            account.save();

            #region Table
            string concaten = account.FirstName + " " + account.LastName;
            var table = new ConsoleTable("Fullname", "Account Number", "Account Type", "Account Balance");

            table.AddRow(concaten, accountNumber, account.AccountType, account.Balance);
            table.Write();
            #endregion

            Exit_Repeat.Confirm();

        }


        public static void Withdraw()
        {
            List<string> fieldTitles = new List<string>();
            List<string> fields = null;

            fieldTitles.Add("Account Number:");
            fieldTitles.Add("Amount: $");
            bool found = false;
            string confirm;
            Account account = null;
            double amount = 0;
            
            
                while (!found)
                {
                    do
                    {
                        fields = HelperClass.GetInfo(fieldTitles);
                    }
                    while (!Validation.DepositValidation(fields[0], fields[1]));

                    found = File.Exists(string.Format("{0}.txt", fields[0]));

                    if (!found)
                    {
                        Console.WriteLine("Account not found !");
                        Console.Write("Retry (y/n): ");
                        confirm = Console.ReadLine();
                        if (confirm == "y") continue;
                        else return;
                    }
                }
                Console.WriteLine("Account found !");

                found = Validation.WithdrawLimitValidation(fields[0], fields[1]);
            #region delete
            //int accountNumber = int.Parse(fields[0]);
            //amount = double.Parse(fields[1]);

            //This code is to get the account of the person with the account number in the constructor
            //account = new Account(accountNumber);
            //account.load();
            //if (amount > account.Balance)
            //{
            //    Console.WriteLine("Not enough money to withdraw in the account !");
            //    found = false;
            //}
            //if(amount <= 1000)
            //{
            //    Console.WriteLine("Sorry, You can not make withdrawals of $1000 and below");
            //    found= false;
            //}
            //else overdraft = false;
            #endregion

            #region This gets the Account of the Account Number that is passed in as an argument
            int accountNumber = int.Parse(fields[0]);
            amount = double.Parse(fields[1]);

            account = new Account(accountNumber);
            account.load();
            account.withdrawSubtractFromBalance(amount);
            account.save();
            #endregion

            #region table
            string concaten = account.FirstName + " " + account.LastName;

            var table = new ConsoleTable("Fullname", "Account Number", "Account Type", "Account Balance");
            table.AddRow(concaten, account.AccountNumber, account.AccountType, account.Balance);
            table.Write();
            #endregion


            Exit_Repeat.Confirm();
          
        }


     
        
        public static void SearchAccount()
        {
            List<string> fieldTitles = new List<string>();
            List<string> fields = null;
            while (true)
            {
                fieldTitles.Clear();
                fieldTitles.Add("Account Number:");
                bool found = false;
                string confirm;
                while (!found)
                {
                    do
                    {
                        fields = HelperClass.GetInfo(fieldTitles);
                    }
                    while (!Validation.searchAccountValidation(fields[0]));

                    found = File.Exists(string.Format("{0}.txt", fields[0]));

                    if (!found)
                    {
                        Console.WriteLine("Account not found !");
                        Console.Write("Check another account (y/n): ");
                        confirm = Console.ReadLine();
                        if (confirm == "y") continue;
                        else return;
                    }
                }
                Console.WriteLine("Account found !");
                int accountNumber = int.Parse(fields[0]);
                Account account = new Account(accountNumber);
                account.load();
                
                #region delete
                //fieldTitles.Clear();
                //fieldTitles.Add("Account No: " + account.AccountNumber);
                //fieldTitles.Add("Account Balance: $" + account.Balance);
                //fieldTitles.Add("First Name: " + account.FirstName);
                //fieldTitles.Add("Last Name: " + account.LastName);
                //fieldTitles.Add("Address: " + account.AccountType);
                #endregion

                #region Table
                string concaten = account.FirstName + " " + account.LastName;
                var table = new ConsoleTable("Fullname", "Account Number", "Account Type", "Account Balance");

                table.AddRow(concaten, accountNumber, account.AccountType, account.Balance);
                table.Write();
                #endregion

                Exit_Repeat.Confirm();

            }


        }


    }
}
