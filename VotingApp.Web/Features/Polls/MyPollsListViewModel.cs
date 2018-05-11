using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Web.Features.Polls
{
    public class MyPollsListViewModel
    {
        public string Question { get; set; }
        public int TotalVotes { get; set; }
    }
}
