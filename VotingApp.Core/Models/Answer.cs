using System;
using System.Collections.Generic;
using System.Text;

namespace VotingApp.Core.Models
{
    public class Answer
    {
        public int AnswerId { get; set; } // primary key by ef core conventions
        public string Description { get; set; }
        public int Votes { get; set; }
        public Poll Poll { get; set; }
    }
}
