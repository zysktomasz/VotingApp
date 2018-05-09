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
    }
}
