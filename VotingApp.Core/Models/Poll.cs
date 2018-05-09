using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingApp.Core.Models
{
    public class Poll
    {
        public int PollId { get; set; } // primary key by ef core conventions
        public string Question { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } // cascade on delete by ef core conventions
        public PollStatus Status { get; set; }
    }

    public enum PollStatus
    {
        Public,
        Private
    }
}
