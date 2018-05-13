using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Core.Interfaces;
using VotingApp.Web.Features.Polls;

namespace VotingApp.Web.Features.Home
{

    public class HomeController : Controller
    {
        private readonly IPoll _polls;

        public HomeController(IPoll polls)
        {
            _polls = polls;
        }

        public IActionResult Index()
        {
            var model = _polls.GetAllPublic()
                .Select(poll => new PollsListViewModel
                {
                    Id = poll.PollId,
                    Question = poll.Question,
                    TotalVotes = poll.Answers.Sum(answer => answer.Votes)
                });

            return View(model);
        }
    }
}