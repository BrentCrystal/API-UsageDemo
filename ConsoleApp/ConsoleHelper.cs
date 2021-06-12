using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class ConsoleHelper
    {
        public static (int id, string quit) GetPersonIdOrQuit(this string message)
        {
            var output = (id: 0, quit: "");

            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                output.quit = "exit";
            }

            bool isvalidInt = int.TryParse(input, out output.id);

            while (isvalidInt == false && output.quit != "exit")
            {
                Console.WriteLine(message);
                isvalidInt = int.TryParse(Console.ReadLine(), out output.id);
            }

            return output;
        }

        public static void PrintPerson(this PersonModel person)
        {
            Type t = person.GetType();
            PropertyInfo[] properties = t.GetProperties();

            foreach (PropertyInfo p in properties)
            {
                if (p.PropertyType.IsArray)
                {
                    Array a = (Array)p.GetValue(person);
                    for (int i = 0; i < a.Length; i++)
                    {
                        Console.WriteLine(p.Name + " : " + a.GetValue(i));
                    }
                }
                else
                {
                    Console.WriteLine(p.Name + " : " + p.GetValue(person));
                }
            }
        }
    }
}
