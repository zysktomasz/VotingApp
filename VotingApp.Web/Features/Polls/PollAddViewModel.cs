using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Web.Features.Polls
{
    public class PollAddViewModel
    {
        [Required]
        public string Question { get; set; }
        [EnsureICollectionElementNotNull(ErrorMessage = "Answer cannot be empty.")]
        public ICollection<string> Answers { get; set; }
        public PollStatus Status { get; set; }
    }

    public class EnsureICollectionElementNotNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as ICollection<string>;
            if (list == null) return false;

            if (list.Any(answer => answer == null))
            {
                return false;
            }
            return true;
        }
    }

}
