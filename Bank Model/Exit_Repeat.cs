using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Model
{
    internal class Exit_Repeat
    {
        public static void Confirm()
        {

            #region Feilds
            bool choice = false;
            int result = 0;
            #endregion
            do
            {
                Console.WriteLine(@"Do You Want To Perform Another Transaction?
Press 1 ===> yes
Press 2 ===> No
");

                choice = int.TryParse(Console.ReadLine(), out result);
                if (result < 1 || result > 2)
                {
                    choice = false;
                }

            } while (!choice);


            if(result == 1) 
            {
                Menu.AccountMenu();
            }
            else
            {
                Console.WriteLine("Thank You for Banking With us");
            }
          
        }

     
    }
}
