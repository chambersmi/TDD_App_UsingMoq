using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Customers.API.Models;

namespace TDD_Customers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
            {
                new User
                {
                    Name = "Kaleb",
                    Email = "k@k.com",
                    Address = new Address
                    {
                        Street = "123 Whatever St",
                        City = "Phoenix",
                        ZipCode = 65498
                    }
                },

                new User
                {
                    Name = "Oreo",
                    Email = "oreo@meow.com",
                    Address = new Address
                    {
                        Street = "123 Eatsalot Ave",
                        City = "Yum",
                        ZipCode = 46544
                    }
                },

                new User
                {
                    Name = "Pebble",
                    Email = "meow@cat.org",
                    Address = new Address
                    {
                        Street = "123 Fluffy St",
                        City = "Catsland",
                        ZipCode = 34343
                    }
                }
            };
    }
}
