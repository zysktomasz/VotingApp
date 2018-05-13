using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Web.Features.Polls
{
    public class PollVoteViewModel
    {
        public int PollId { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
