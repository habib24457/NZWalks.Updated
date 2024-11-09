using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext: DbContext
	{
		//shortcuts for creating a contructor ctor+double tab
		public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}

		//Creates table in the sql database
		public DbSet<Difficulty> Diffculties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			//Seed the data for Difficulties property
			//Difficulties = Easy, Medium, Hard

			var difficulties = new List<Difficulty>()
			{
				// we need unique id for each difficulty level, but the should not change each time
				// We can generate unique id using c# interactive: view > other windows > C# interactive window
				new Difficulty()
				{
					Id = Guid.Parse("d37fa264-31a4-4156-b010-13b52c4f6ee9"),
					Name = "Easy"
				},
                 new Difficulty()
                {
                    Id =Guid.Parse("30711259-7265-4794-af0d-be1057745b46"),
                    Name = "Medium"
                },

                new Difficulty()
                {
                    Id =Guid.Parse("bd165a93-cc41-4d45-852a-26c6049b0411"),
                    Name = "Hard"
                }
            };

			modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}

