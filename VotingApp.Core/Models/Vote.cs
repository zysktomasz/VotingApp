using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VotingApp.Core.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public string UserId { get; set; } // ApplicationUser.Id
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Poll Poll { get; set; }
        public Answer Answer { get; set; }
    }
}
