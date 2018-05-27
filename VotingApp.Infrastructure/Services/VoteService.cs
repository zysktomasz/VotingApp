using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Core.Interfaces;
using VotingApp.Core.Models;
using VotingApp.Infrastructure.Data;

namespace VotingApp.Infrastructure.Services
{
    public class VoteService : IVote
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddVote(Vote newVote)
        {
            _context.Add(newVote);

            var answer = newVote.Answer;
            answer.Votes += 1;

            _context.SaveChanges();
        }

        public bool CheckIfUserAlreadyVoted(int pollId, ApplicationUser user)
        {
            return _context.Votes.Any(vote => vote.PollId == pollId && vote.User == user);
        }
    }
}
