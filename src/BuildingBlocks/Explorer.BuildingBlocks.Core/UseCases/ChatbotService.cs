using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Concurrent;
using System.Collections;



namespace Explorer.BuildingBlocks.Core.UseCases
{
    public class ChatbotService : IChatbotService
    {
        private Dictionary<string, List<string>> questionSets;
        private Dictionary<string, string> rootSet;
        private Dictionary<string, string> toursSet;
        private Dictionary<string, string> tourExecutionSet;
        private Dictionary<string, string> allSets;

        private Dictionary<string, string> searchSets;
        private FuzzySearcher fuzzySearcher;

        public ChatbotService()
        {
            allSets = new Dictionary<string, string>();
            fuzzySearcher = new FuzzySearcher();
            BuildQuestionTree();
           
        }
        public string GetResponse(string userMessage, long userId)
        {


            if (allSets.TryGetValue(userMessage, out string answer))
            {
                return answer;
            }
            return "I didn't understand that.";



        }

        public List<string> GetQuestionSet(string setTag)
        {
            if (questionSets.TryGetValue(setTag, out List<string> questions))
            {
                return questions;
            }

            List<string> placeholder = new List<string> {"No questions" };
            return placeholder;

        }

        private void BuildQuestionTree()
        {
            rootSet = new Dictionary<string, string>
            {
                {"About the app",  "This app simplifies your travel experience by allowing you to browse and buy tours, start guided tours, and engage in exciting encounters—all in one place. Would you like to know more about developers?"},
                {"Tours",  "Users can buy a variety of tours, including guided and self-paced options, featuring keypoints at popular landmarks, scenic locations, and cultural hotspots."},
                {"Encounters",  "You can discover social encounters with other users and unlock hidden encounters at secret locations for a unique and engaging experience."}
            };

            toursSet = new Dictionary<string, string>
            {
                {"Where can I find tours?",  "You can find tours on the Browse Tours page on the Navbar."},
                {"How to start a tour?",  "You can start a tour after you you bought it, on My tours page, by clicking on Start a tour button. "},
                {"How to buy a tour?", "You can buy a tour on the Browse Tours page. After clicking on 'Buy', the tour will appear in your cart." }
            };

            tourExecutionSet = new Dictionary<string, string>
            {
                {"How to complete a tour?",  "You can follow keypoints in the Position Simulator, located on Navbar."},
                {"How to abandon a tour?", "You can click on Abandon tour button." }
            };


            questionSets = new Dictionary<string, List<string>>
            {
                {"ROOT", new List<string>(rootSet.Keys) },
                {"TOURS", new List<string>(toursSet.Keys) },
                {"TOUR_EXECUTIONS", new List<string>(tourExecutionSet.Keys) },
            };

           


            AddToAllSets(rootSet);
            AddToAllSets(toursSet);
            AddToAllSets(tourExecutionSet);

            
        }

        private void AddToAllSets(Dictionary<string, string> questionAnswerSet)
        {
            foreach (var question_answer in questionAnswerSet)
            {
                if (!allSets.ContainsKey(question_answer.Key))
                {
                    allSets.Add(question_answer.Key, question_answer.Value);
                }
            }
        }

        public List<string> GetSearchedQuestions(string query)
        {
            List<string> allQuestions = allSets.Keys.ToList();
            List<string> result = fuzzySearcher.FuzzySearch(query, allQuestions);

            return result;

        }
    }
}
