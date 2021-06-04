using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class ConsoleHelper
    {
        public static int GetPersonId(this String message)
        {
            int output = 0;

            Console.WriteLine(message);

            bool isvalidInt = int.TryParse(Console.ReadLine(), out output);

            while (isvalidInt == false)
            {
                Console.WriteLine(message);
                isvalidInt = int.TryParse(Console.ReadLine(), out output);
            }

            return output;
        }
    }
}
