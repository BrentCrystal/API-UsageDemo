using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.InitializeClient();

            bool isPersonId = true;

            do
            {
                (int id, string quit ) userInput = "Enter Id number for Star Wars Character or Exit to Quit:".GetPersonIdOrQuit();

                if (userInput.quit =="exit")
                {
                    isPersonId = false;
                }
                else
                {
                    try
                    {
                        var person = GetOrCreateCache(userInput.id).Result;
                        person.PrintPerson();
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Person not found");
                        Console.WriteLine();
                    }
                }
                                
            } while (isPersonId);
        }

        public static async Task<PersonModel> GetOrCreateCache(int personId)
        {
            var peopleCache = new MemoryCache<PersonModel>();
            var person = await peopleCache.GetOrCreate(personId, async () => await PeopleProcessor.LoadPerson(personId));
          
            return person;
        }
    }
}
