using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Core.Interfaces;
using VotingApp.Core.Models;
using VotingApp.Infrastructure.Data;

namespace VotingApp.Web.Services
{
    public class PollService : IPoll
    {
        private readonly ApplicationDbContext _context;

        public PollService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Poll newPoll)
        {
            _context.Add(newPoll);
            _context.SaveChanges();
        }

        public Poll GetPollById(int pollId)
        {
            return _context.Polls
                    .Include(poll => poll.Answers)
                .FirstOrDefault(poll => poll.PollId == pollId);
        }

        public IEnumerable<Poll> GetPollsByAuthorId(string userId)
        {
            return _context.Polls
                    .Include(poll => poll.Answers)
                .Where(poll => poll.UserId == userId)
                .OrderByDescending(poll => poll.PollId);
        }

    }
}
