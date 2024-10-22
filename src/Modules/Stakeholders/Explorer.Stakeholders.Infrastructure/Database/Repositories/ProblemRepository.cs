using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemRepository: IProblemRepository
    {
        private readonly StakeholdersContext _db;

        public ProblemRepository(StakeholdersContext db)
        {
            _db = db;
        }
        public List<Problem> GetByUserId(long id)
        {
            var problems = new List<Problem>();
            foreach(var p in _db.Problem)
            {
                if (p.UserId == id)
                    problems.Add(p);
            }

            return problems;
            //return _db.Problem.Where(t => t.UserId == id).ToList();
        }
        public List<Problem> GetByTourId(long id)
        {
            return _db.Problem.Where(t => t.TourId == id).ToList();
        }
    }
}
