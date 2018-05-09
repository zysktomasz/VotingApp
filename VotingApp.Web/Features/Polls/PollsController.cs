using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VotingApp.Web.Features.Polls
{
    [Authorize]
    public class PollsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPoll()
        {
            return View();
        }
    }
}