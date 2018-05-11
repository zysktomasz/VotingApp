using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Web.Features.Polls
{
    public class MyPollsItemViewModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
