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
        private readonly IVote _votes;

        public PollsController(UserManager<ApplicationUser> userManager, IPoll polls, IVote votes)
        {
            _userManager = userManager;
            _polls = polls;
            _votes = votes;
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

        public IActionResult AddPoll()
        {
            return View(new PollAddViewModel());
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
                return RedirectToAction(nameof(AddPoll));
            }

            return View(model);
        }

        public async Task<IActionResult> MyPolls()
        {
            var currentUserId = (await _userManager.GetUserAsync(User)).Id;
            var userPolls = _polls.GetPollsByAuthorId(currentUserId)
                    .Select(poll => new PollsListViewModel
                    {
                        Id = poll.PollId,
                        Question = poll.Question,
                        TotalVotes = poll.Answers.Sum(answer => answer.Votes)
                    }).ToList();

            return View(userPolls);
        }

        [Route("[controller]/[action]/{pollId}")]
        public IActionResult MyPolls(int pollId)
        {
            var poll = _polls.GetPollById(pollId);
            var model = new MyPollsItemViewModel
            {
                Id = poll.PollId,
                Question = poll.Question,
                Answers = poll.Answers.ToList()
            };

            return View("~/Features/Polls/MyPollsItem.cshtml", model);
        }

        [Route("[controller]/[action]/{pollId}")]
        public IActionResult Vote(int pollId, bool showResults = false)
        {
            var poll = _polls.GetPollById(pollId);

            var model = new PollVoteViewModel
            {
                PollId = poll.PollId,
                Question = poll.Question,
                Answers = poll.Answers.ToList()
            };

            if (showResults == true)
            {
                return View("~/Features/Polls/VoteResults.cshtml", model);
            }
            else
            {
                return View("~/Features/Polls/VoteForm.cshtml", model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("[controller]/[action]/{pollId}")]
        public async Task<IActionResult> Vote(int pollId, int answer)
        {
            var poll = _polls.GetPollById(pollId); // lets me include reference to poll & answer in newVote
            var currentUser = await _userManager.GetUserAsync(User);

            if (_votes.CheckIfUserAlreadyVoted(pollId, answer, currentUser))
            {
                ModelState.AddModelError("", "You have already voted in this poll.");   
            }

            if (ModelState.IsValid)
            {
                Vote newVote = new Vote
                {
                    User = currentUser,
                    PollId = pollId,
                    AnswerId = answer
                };

                _votes.AddVote(newVote);
                return RedirectToAction(nameof(Vote), new { pollid = pollId, showResults = true });
            }

            var model = new PollVoteViewModel
            {
                PollId = poll.PollId,
                Question = poll.Question,
                Answers = poll.Answers.ToList()
            };

            return View("~/Features/Polls/VoteForm.cshtml", model);
        }
    }
}