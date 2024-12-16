using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Concurrent;



namespace Explorer.BuildingBlocks.Core.UseCases
{
    public class ChatbotService : IChatbotService
    {
        private readonly DecisionTreeNode _root;
        private readonly ConcurrentDictionary<long, DecisionTreeNode> _userContexts;

        public ChatbotService()
        {
            _root = BuildDecisionTree();
            _userContexts = new ConcurrentDictionary<long, DecisionTreeNode>();
        }
        public async Task<string> GetResponseAsync(string userMessage, long userId)
        {
           
            await Task.Delay(100);

            var currentNode = _userContexts.GetOrAdd(userId, _root);

            if (userMessage.Equals("Back", StringComparison.OrdinalIgnoreCase))
            {
                if(currentNode.Parent != null)
                {
                    _userContexts[userId] = currentNode.Parent;
                    return currentNode.Parent.Message;
                }

                return "You're already at the main menu.";

            }


            if (currentNode.Responses.TryGetValue(userMessage, out var nextNode))
            {
                _userContexts[userId] = nextNode; 
                return nextNode.Message;
            }

            return "I didn't understand that. Please try again.";


        }

        private DecisionTreeNode BuildDecisionTree()
        {
            var root = new DecisionTreeNode("Hi, how can I help you?", null);

            var appNode = new DecisionTreeNode("This app simplifies your travel experience by allowing you to browse and buy tours, start guided tours, and engage in exciting encounters—all in one place. Would you like to know more about developers?", root);
            var toursNode = new DecisionTreeNode("Users can buy a variety of tours, including guided and self-paced options, featuring keypoints at popular landmarks, scenic locations, and cultural hotspots.", root);
            var encountersNode = new DecisionTreeNode("You can discover social encounters with other users and unlock hidden encounters at secret locations for a unique and engaging experience.", root);

            var findToursNode = new DecisionTreeNode("You can find tours on the Browse Tours page on the Navbar.", toursNode);
            var startTourNode = new DecisionTreeNode("You can start a tour after you you bought it, on My tours page, by clicking on Start a tour button. ", findToursNode);

            var completeTourNode = new DecisionTreeNode("You can follow keypoints in the Position Simulator, located on Navbar.", startTourNode);
            var abandonTourNode = new DecisionTreeNode("You can click on Abandon tour button.", startTourNode);

            root.Responses.Add("About the app", appNode);
            root.Responses.Add("Tours", toursNode);
            root.Responses.Add("Encounters", encountersNode);

            toursNode.Responses.Add("Where can I find tours?", findToursNode);
            toursNode.Responses.Add("How to start a tour", startTourNode);
            startTourNode.Responses.Add("How to complete a tour?", completeTourNode);
            startTourNode.Responses.Add("How to abandon a tour?", abandonTourNode);

            return root;


        }

    }
}
