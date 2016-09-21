using LRSimulator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LRSimulator.DAL
{
    public static class MTGAPI
    {
        public static List<Card> GetCardsBySetCode(string setCode = "")
        {
            List<Card> cardList = new List<Card>();

            using (var client = new HttpClient())
            {
                int page = 1;
                IEnumerable<string> totalCountVals;

                client.BaseAddress = new Uri("https://api.magicthegathering.io/v1/cards");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(string.Format("?set={0}&page={1}", setCode.Trim(), page)).Result;

                if (response.IsSuccessStatusCode)
                {
                    response.Headers.TryGetValues("total-count", out totalCountVals);
                    var totalCount = int.Parse(totalCountVals.First());

                    while (cardList.Count < totalCount)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        var cards = JsonConvert.DeserializeObject<CardDTO>(data);
                        cardList.AddRange(cards.Cards);

                        response = client.GetAsync(string.Format("?set={0}&page={1}", setCode.Trim(), ++page)).Result;
                    }
                }
                
            }

            return cardList;
        }

        public static Set GetSetBySetCode(string setCode = "")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.magicthegathering.io/v1/sets/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(setCode.Trim()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var set = JsonConvert.DeserializeObject<SetDTO>(data);
                    return set.Set;
                }
            }

            return new Set();
        }

        public static List<Set> GetAllSets()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.magicthegathering.io/v1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("sets").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var set = JsonConvert.DeserializeObject<SetListDTO>(data);
                    return set.sets;
                }
            }

            return new List<Set>();
        }
    }
}
