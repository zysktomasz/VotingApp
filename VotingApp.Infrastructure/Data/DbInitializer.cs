using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Infrastructure.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            if(context.Polls.Any())
            {
                return;
            }

            // seed users

            ApplicationUser user1 = new ApplicationUser();
            user1.Email = "adam@localhost";
            user1.UserName = user1.Email;

            await userManager.CreateAsync(user1, "haslo123");

            ApplicationUser user2 = new ApplicationUser();
            user2.Email = "ewa@localhost";
            user2.UserName = user2.Email;


            await userManager.CreateAsync(user2, "haslo123");

            ApplicationUser user3 = new ApplicationUser();
            user3.Email = "johnnnn@localhost";
            user3.UserName = user3.Email;

            await userManager.CreateAsync(user3, "haslo123");


            // seed Polls

            var polls = new Poll[]
            {
                new Poll()
                {
                    Question = "Jaki jest twoj ulubiony film Quentina Tarantino?",
                    Answers = new List<Answer>(),
                    Status = PollStatus.Public,
                    UserId = user1.Id
                },
                new Poll()
                {
                    Question = "Ile masz lat?",
                    Answers = new List<Answer>(),
                    Status = PollStatus.Public,
                    UserId = user1.Id
                },
                new Poll()
                {
                    Question = "Preferowana pora roku?",
                    Answers = new List<Answer>(),
                    Status = PollStatus.Public,
                    UserId = user2.Id
                }
            };

            foreach (var poll in polls)
            {
                context.Polls.Add(poll);
            }


            // seed answers

            var answers = new Answer[]
            {
                new Answer { Description = "Pulp Fiction", Poll = polls[0] },
                new Answer { Description = "Reservoir Dogs", Poll = polls[0] },
                new Answer { Description = "Kill Bill vol 1", Poll = polls[0] },
                new Answer { Description = "Kill Bill vol 2", Poll = polls[0] },
                new Answer { Description = "Inglourious Bastards", Poll = polls[0] },
                new Answer { Description = "Django Unchained", Poll = polls[0] },

                new Answer { Description = "<18", Poll = polls[1] },
                new Answer { Description = "18-30", Poll = polls[1] },
                new Answer { Description = "30+", Poll = polls[1] },

                new Answer { Description = "Zima", Poll = polls[2] },
                new Answer { Description = "Wiosna", Poll = polls[2] },
                new Answer { Description = "Lato", Poll = polls[2] },
                new Answer { Description = "Jesien", Poll = polls[2] }
            };

            foreach (var answer in answers)
            {
                context.Answers.Add(answer);
            }


            // seed votes

            context.SaveChanges();

        }
    }
}
