using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Numerics;

namespace Bank_Model
{
    public class Account
    {
        public int accountNumber;
        public double balance;
        public string firstName;
        public string lastName;
        public string accountType;
        private string Note;

        public Account(int accountNumber, double balance, string firstName, string lastName, string accountType)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
            this.accountType = accountType;
        }

        public Account(int accountNumber, double balance, string firstName, string lastName, string accountType, string Note)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
            this.firstName = firstName;
            this.lastName = lastName;
            this.accountType = accountType;
            this.Note = Note;
        }


        public Account(int accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public Account()
        { 
            
        }
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public void save()
        {
            TextWriter writer = new StreamWriter(string.Format("{0}.txt", accountNumber));
            //no need to write account number, since file name already contains it           
            writer.WriteLine(balance);
            writer.WriteLine(firstName);
            writer.WriteLine(lastName);
            writer.WriteLine(accountType);
            writer.WriteLine(Note);
            writer.Close();
        }

        public void load()
        {
            TextReader reader = new StreamReader(string.Format("{0}.txt", accountNumber));
            balance = double.Parse(reader.ReadLine());
            firstName = reader.ReadLine();
            lastName = reader.ReadLine();
            accountType = reader.ReadLine();
            Note = reader.ReadLine();
            reader.Close();
        }


        

        public void depositAddToBalance(double amount)
        {
            if(amount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            balance += amount;
        }
        public void withdrawSubtractFromBalance(double amount)
        {
            balance -= amount;
        }
      

    }
}
