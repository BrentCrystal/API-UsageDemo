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

            PersonModel person = new PersonModel();

            int personId = "Enter an Id number to return Star Wars character data:".GetPersonId();

            try
            {
                person = GetPersonData(personId);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Person not found");
            }

            Console.WriteLine();
            PrintPersonData(person);

            Console.ReadLine();
        }

        private static void PrintPersonData(PersonModel person)
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

        private static PersonModel GetPersonData(int personId)
        {
            var person = PeopleProcessor.LoadPerson(personId).Result;

            return person;
        }
    }
}
