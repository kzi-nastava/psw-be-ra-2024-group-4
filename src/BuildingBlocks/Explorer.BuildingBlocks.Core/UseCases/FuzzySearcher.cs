using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuoVia.FuzzyStrings;
namespace Explorer.BuildingBlocks.Core.UseCases
{
    public class FuzzySearcher
    {
        public List<string> FuzzySearch(string searchTerm, List<string> data)
        {
            List<string> result = new List<string>();

            foreach(string s in data)
            {
                if(s.FuzzyMatch(searchTerm) > 0.1 && result.Count < 3) 
                    result.Add(s);
            }

            return result;
        }
    }
}
