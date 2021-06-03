using DemoLibrary;
using System;
using System.Collections.Generic;
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
                var person = PeopleProcessor.LoadPerson(1).Result;

                Type t = person.GetType();
                PropertyInfo[] properties = t.GetProperties();

                foreach (PropertyInfo p in properties)
                {
                    if (p.PropertyType.IsArray )
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
            catch (Exception ex)
            {
                Console.WriteLine("Person not found");
            }

            Console.ReadLine();
        }

       
    }
}
