using BensBlazorWebAssembly.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace BensBlazorWebAssembly.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VideoGame>().HasData(GetGames());
        }

        public DbSet<VideoGame> Games { get; set; }

        /// <summary>
        /// Get a list of games
        /// as this does not create an instance of the class, it is static
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<VideoGame> GetGames()
        {
            var listOfGames = new List<VideoGame>();
            listOfGames.Add(new VideoGame { Id = 1, Title = "Halo", Genre = "Shooter", Platform = Common.Enums.Platform.XboxOne, AgeRestriction = Common.Enums.AgeRestriction.Eighteen });
            listOfGames.Add(new VideoGame { Id = 2, Title = "Mario", Genre = "Platformer", Platform = Common.Enums.Platform.NintendoSwitch, AgeRestriction = Common.Enums.AgeRestriction.Three });
            listOfGames.Add(new VideoGame { Id = 3, Title = "God of War", Genre = "Action", Platform = Common.Enums.Platform.PS4, AgeRestriction = Common.Enums.AgeRestriction.Eighteen });
            listOfGames.Add(new VideoGame { Id = 4, Title = "Fortnite", Genre = "Battle Royale", Platform = Common.Enums.Platform.Android, AgeRestriction = Common.Enums.AgeRestriction.Twelve });
            listOfGames.Add(new VideoGame { Id = 5, Title = "Minecraft", Genre = "Sandbox", Platform = Common.Enums.Platform.Windows, AgeRestriction = Common.Enums.AgeRestriction.Seven });
            listOfGames.Add(new VideoGame { Id = 6, Title = "Roblox", Genre = "Sandbox", Platform = Common.Enums.Platform.Windows, AgeRestriction = Common.Enums.AgeRestriction.None });
            return listOfGames;
        }
    }
}
