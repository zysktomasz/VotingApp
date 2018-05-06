using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VotingApp.Web.Features.Home
{

    public class HomeController : Controller
    {
        public string Index()
        {
            return "test";
        }
    }
}