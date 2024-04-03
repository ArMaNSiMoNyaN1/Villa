using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace Villa.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbContext<Villa> Villas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Villa>().HasData(
            new Models.Villa()
            {
                Id = 1,
                Name = "Royal villa",
                Details = "Luxury villa with pool",
                ImageUrl =
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/295090917.jpg?k=d17621b71b0eaa0c7a37d8d8d02d33896cef75145f61e7d96d296d88375a7d39&o=&hp=1",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedDate = DateTime.Now
            },
          new Models.Villa()
            {
                Id = 2,
                Name = "Luxury villa",
                Details = "Luxury villa with swimming pool and modern design",
                ImageUrl =
                    "https://q-xx.bstatic.com/xdata/images/hotel/max1024x768/314234927.jpg?k=e8d0ff333645000b3345bf3c924ebdba3547cd73c362881b16049f5fe5d19701&o=",
                Occupancy = 5,
                Rate = 400,
                Sqft = 600,
                Amenity = "",
                CreatedDate = DateTime.Now
            },
          new Models.Villa()
            {
                Id = 3,
                Name = "Biggest villa",
                Details = "The largest and most luxurious villa with swimming pool and modern design",
                ImageUrl =
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/301483778.jpg?k=b1f449beb857de98e8287c34956b672956926c2d03ac185ff8d9a348622c157a&o=&hp=1",
                Occupancy = 5,
                Rate = 600,
                Sqft = 900,
                Amenity = "",
                CreatedDate = DateTime.Now
            });
    }
}