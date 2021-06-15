using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class PeopleProcessor
    {
        public static async Task<PersonModel> LoadPerson(int personId = 0)
        {
            string url = "";

            if (personId > 0)
            {
                url = $"https://swapi.dev/api/people/ {personId}/";
            }
            else
            {
                url = $"https://swapi.dev/";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PersonModel person = await response.Content.ReadAsAsync<PersonModel>();

                    return person;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
