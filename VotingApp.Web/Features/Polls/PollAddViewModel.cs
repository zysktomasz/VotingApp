using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Web.Features.Polls
{
    public class PollAddViewModel
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public ICollection<string> Answers { get; set; }

    }
}
