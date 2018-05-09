using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Core.Interfaces;
using VotingApp.Core.Models;

namespace VotingApp.Web.Features.Polls
{
    [Authorize]
    public class PollsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPoll _polls;

        public PollsController(UserManager<ApplicationUser> userManager, IPoll polls)
        {
            _userManager = userManager;
            _polls = polls;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPoll()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPoll([Bind("Question,Answers")] PollAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Poll newPoll = new Poll
                {
                    Question = model.Question,
                    Answers = model.Answers.Select(s => new Answer
                    {
                        Description = s,
                    }).ToList(),
                    Status = PollStatus.Public,
                    User = await _userManager.GetUserAsync(User)
                };

                _polls.Add(newPoll);
                return View();
            }

            return View(model);
        }
    }
}