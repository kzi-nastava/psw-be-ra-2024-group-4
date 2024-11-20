using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;



namespace Explorer.BuildingBlocks.Core.UseCases
{
    public class ChatbotService : IChatbotService
    {
        private readonly Dictionary<string, string> _responseDictionary;
        private readonly HttpClient _httpClient;

        public ChatbotService()
        {
             _responseDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
             {
                 { "Where can I find tours?", "You can find tours on the Browse Tours page on Navbar." },
                 { "How do I buy a tour?", "To buy a tour, go to the Browse Tours section and click on buy. You can see yout items in a Shopping cart. Click on checkout and buy tours." },
                 { "How do I start a tour", "To start a tour, go to the My Tours section and click on Start tour. To follow the keypoints, go to PositionSimulator." }
             };

            _httpClient = new HttpClient();
        }
        public async Task<string> GetResponseAsync(string userMessage)
        {
           
            await Task.Delay(100);

             if (_responseDictionary.ContainsKey(userMessage))
             {
                 return _responseDictionary[userMessage];
             }

             var match = _responseDictionary.Keys
               .FirstOrDefault(key => IsFuzzyMatch(userMessage, key));

             if (match != null)
             {
                 return _responseDictionary[match];
             }

             return "I don't understand the question.";

          
        }

        private bool IsFuzzyMatch(string input, string key)
        {
           
            return input.Contains(key, StringComparison.OrdinalIgnoreCase);
        }
    }
}
