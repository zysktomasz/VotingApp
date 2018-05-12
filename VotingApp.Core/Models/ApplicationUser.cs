using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VotingApp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Poll> Polls { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
