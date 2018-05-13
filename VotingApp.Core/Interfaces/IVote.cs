using System;
using System.Collections.Generic;
using System.Text;
using VotingApp.Core.Models;

namespace VotingApp.Core.Interfaces
{
    public interface IVote
    {
        void AddVote(Vote newVote);
    }
}
