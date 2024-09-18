using Lemax_Test_Assignment.DTOs;
using Lemax_Test_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemax_Test_Assignment.Tests.Data
{
  public static class TestData
  {
    public static IEnumerable<Hotel> GetSampleHotels()
    {
      return new List<Hotel>
            {
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Paris", Price = 150.00m, Location = new GeoLocation { Latitude = 48.8566, Longitude = 2.3522 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Berlin", Price = 120.00m, Location = new GeoLocation { Latitude = 52.52, Longitude = 13.405 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Rome", Price = 180.00m, Location = new GeoLocation { Latitude = 41.9028, Longitude = 12.4964 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Madrid", Price = 130.00m, Location = new GeoLocation { Latitude = 40.4168, Longitude = -3.7038 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Amsterdam", Price = 140.00m, Location = new GeoLocation { Latitude = 52.3676, Longitude = 4.9041 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Brussels", Price = 110.00m, Location = new GeoLocation { Latitude = 50.8503, Longitude = 4.3517 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Vienna", Price = 160.00m, Location = new GeoLocation { Latitude = 48.2082, Longitude = 16.3738 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Zurich", Price = 170.00m, Location = new GeoLocation { Latitude = 47.3769, Longitude = 8.5417 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Copenhagen", Price = 140.00m, Location = new GeoLocation { Latitude = 55.6761, Longitude = 12.5683 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Budapest", Price = 120.00m, Location = new GeoLocation { Latitude = 47.4979, Longitude = 19.0402 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Oslo", Price = 150.00m, Location = new GeoLocation { Latitude = 59.9139, Longitude = 10.7522 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Warsaw", Price = 100.00m, Location = new GeoLocation { Latitude = 52.2297, Longitude = 21.0122 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Prague", Price = 130.00m, Location = new GeoLocation { Latitude = 50.0755, Longitude = 14.4378 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Lisbon", Price = 140.00m, Location = new GeoLocation { Latitude = 38.7223, Longitude = -9.1393 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Edinburgh", Price = 160.00m, Location = new GeoLocation { Latitude = 55.9533, Longitude = -3.1883 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Dubrovnik", Price = 180.00m, Location = new GeoLocation { Latitude = 42.6507, Longitude = 18.0944 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Tallinn", Price = 130.00m, Location = new GeoLocation { Latitude = 59.4370, Longitude = 24.7535 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Reykjavik", Price = 200.00m, Location = new GeoLocation { Latitude = 64.1355, Longitude = -21.8954 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Ljubljana", Price = 140.00m, Location = new GeoLocation { Latitude = 46.0511, Longitude = 14.5051 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel Valletta", Price = 150.00m, Location = new GeoLocation { Latitude = 35.8997, Longitude = 14.5147 } },
                new Hotel { Id = Guid.NewGuid(), Name = "Hotel San Marino", Price = 170.00m, Location = new GeoLocation { Latitude = 43.9333, Longitude = 12.4500 } }
            };
    }
  }
}
