namespace LiverpoolFanSite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TablesServicesTest
    {
        [Fact]
        public void TestGetAllTeamsWithZeroTeams()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));

            var tablesService = new TablesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestTables).Assembly);

            Assert.Empty(tablesService.GetAll<MyTestTables>());
        }

        [Fact]
        public void TestGetAllWithSingleTeam()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Team>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Team { Name = "Livepool" });
            repository.SaveChangesAsync();
            var tablesService = new TablesService(repository);

            Assert.Single(tablesService.GetAll<MyTestTables>());
        }

        public class MyTestTables : IMapFrom<Team>
        {
            public string Name { get; set; }

            public int Wins { get; set; }

            public int Draws { get; set; }

            public int Loses { get; set; }

            public int GoalsScored { get; set; }

            public int GoalsAgainst { get; set; }

            public int GoalDifference => this.GoalsScored - this.GoalsAgainst;

            public int Points { get; set; }
        }
    }
}
