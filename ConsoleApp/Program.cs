using DemoLibrary;
using System;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();

            try
            {
                var person = PeopleProcessor.LoadPerson(32).Result;

                Type t = person.GetType();
                PropertyInfo[] pi = t.GetProperties();

                foreach (PropertyInfo p in pi)
                {
                    Console.WriteLine(p.Name + " : " + p.GetValue(person));
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Person not found");
            }

            Console.ReadLine();
        }

       
    }
}
