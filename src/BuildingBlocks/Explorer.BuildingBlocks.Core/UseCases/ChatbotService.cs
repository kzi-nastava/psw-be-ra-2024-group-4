using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.UseCases
{
    public class ChatbotService : IChatbotService
    {
        public async Task<string> GetResponseAsync(string userMessage)
        {
           
            await Task.Delay(100);
            return $"You said: {userMessage}";
        }
    }
}
