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
        private Dictionary<string, string> couponSet;
        private Dictionary<string, string> tourPurchaseSet;
        private Dictionary<string, string> blogSet;
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
                /*{"About the app",  "This app simplifies your travel experience by allowing you to browse and buy tours, start guided tours, and engage in exciting encounters—all in one place. Would you like to know more about developers?"},*/
                {"Tours",  "Users can buy a variety of tours, including guided and self-paced options, featuring keypoints at popular landmarks, scenic locations, and cultural hotspots."},
                {"Encounters",  "You can discover social encounters with other users and unlock hidden encounters at secret locations for a unique and engaging experience."},
                {"Blogs", "The blog feature lets you share your travel experiences, leave comments and votes, and interact with other users. You can also vote on blogs to show your appreciation or feedback."},
                {"Clubs", "dodati" },
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
            tourPurchaseSet = new Dictionary<string, string>
            {
                {"How to use coupons?" ,"You can see most frequently asked questions about coupons here." },


            };
            couponSet = new Dictionary<string, string>
            {
                {"Where do I enter my coupon?",
                 "You can enter your coupon code during checkout, right below your cart summary."},
                {
                 "Can I apply more than one coupon?",
                 "No, you can only use one coupon code per purchase."},
                {
                  "How can I get a coupon?",
                  "Coupon codes are shared outside this app by the tour authors. Keep an eye on their promotions or announcements."},
            };
            blogSet = new Dictionary<string, string>
            {
                {"How can I leave a comment?","To leave a comment, go to the specific blog post and use the comment form on the left side of the page." },
                { "Can I see which vote I gave on a blog?","Yes, the button you used to upvote or downvote will be highlighted to show your choice."},
                { "What is a 'famous' blog?","A blog is labeled 'famous' if it has a score above 500 and more than 30 comments."},
                {"Can I upvote or downvote multiple times?","No, you can only cast one vote per blog. You can change your vote if needed" },
            };


            questionSets = new Dictionary<string, List<string>>
            {
                {"ROOT", new List<string>(rootSet.Keys) },
                {"TOURS", new List<string>(toursSet.Keys) },
                {"TOUR_EXECUTIONS", new List<string>(tourExecutionSet.Keys) },
                {"TOUR_PURCHASE", new List<string>(tourPurchaseSet.Keys)},
                {"COUPONS", new List<string>(couponSet.Keys) },
                {"BLOGS" , new List<string>(blogSet.Keys) },
            };

           


            AddToAllSets(rootSet);
            AddToAllSets(toursSet);
            AddToAllSets(tourExecutionSet);
            AddToAllSets(tourPurchaseSet);
            AddToAllSets(blogSet);
            AddToAllSets(couponSet);

            
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
