using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Bank_Model
{
    internal class HelperClass : Validation
    {
        private static Random R = new Random();
        private const int min = 100000;
        private const int max = 10000000;

        public static List<string> GetInfo(List<string> feildTitle)
        {
            List<string> info = new List<string>();
            foreach (string item in feildTitle)
            {
                Console.Write(item + " ");
                info.Add(Console.ReadLine());
                Console.WriteLine(" ");
            }
            return info;
        }

        private static string SanitizeName(string name)
        {
            return char.ToUpper(name[0]) + name[1..];
        }


       

       
        public static int GenerateAccount(string fields)
        {
            int id = R.Next(min, max);
            while (File.Exists(string.Format("{0}", id)))
            {
                id = R.Next(min, max);
            }
            if (fields.ToLower() == "savings") return int.Parse("201" + id.ToString());
            return int.Parse("101" + id.ToString());
        }


      

       
    }
}
