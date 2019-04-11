using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MvcEF.Data;
using MvcEF.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcEfCore.Data
{
    public class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any teams.
                if (context.Teams.Any())
                {
                    return;   // DB has already been seeded
                }

                var teams = SeedData.GetTeams().ToArray();
                context.Teams.AddRange(teams);
                context.SaveChanges();

                var players = SeedData.GetPlayers(context).ToArray();
                context.Players.AddRange(players);
                context.SaveChanges();
            }
        }

        public static List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>() {
            new Team() {
                TeamName="IfeanyiUbahFC",
                City="Nnewi",
                Province="Anambra",
                Country="Nigeria",
            },
            new Team() {
                TeamName="Lagos TonadoesFC",
                City="Mushin",
                Province="Lagos",
                Country="Nigeria",
            },
            new Team() {
                TeamName="EnuguRangers",
                City="Enugu",
                Province="Enugu ",
                Country="Nigeria"
            },
            new Team() {
                TeamName="ThokemFc",
                City="Heartland",
                Province="Abuja",
                Country="Nigeria",
            },
            new Team() {
                TeamName="Westham",
                City="West",
                Province="Ham",
                Country="England",
            },
            new Team() {
                TeamName="KanoPillarsFc ",
                City="Kano",
                Province="Kano",
                Country="Nigeria",
            },
            new Team() {
                TeamName="MFM FC",
                City="Mountain",
                Province="MFM",
                Country="Nigeria",
            },
            new Team() {
                TeamName="Manchester United",
                City="Manchester",
                Province="Manchester",
                Country="England",
            },
        };

            return teams;
        }

        public static List<Player> GetPlayers(ApplicationDbContext context)
        {
            List<Player> players = new List<Player>() {
            new Player {
                FirstName = "Ike",
                LastName = "Nwafor",
                TeamName = context.Teams.Find("IfeanyiUbahFC").TeamName,
                Position = "Forward"
            },
            new Player {
                FirstName = "Musa",
                LastName = "Ahmed",
                TeamName = context.Teams.Find("KanoPillarsFC").TeamName,
                Position = "Left Wing"
            },
            new Player {
                FirstName = "Daniel Omacachi",
                LastName = "Rooster",
                TeamName = context.Teams.Find("MFMFC").TeamName,
                Position = "Right Wing"
            },
            new Player {
                FirstName = "Ikemefuna",
                LastName = "Anthony",
                TeamName = context.Teams.Find("MancherUnited").TeamName,
                Position = "Defense"
            },
        };

            return players;
        }
    }
}
