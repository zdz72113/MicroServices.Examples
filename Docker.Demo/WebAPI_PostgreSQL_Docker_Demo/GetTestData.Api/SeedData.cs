using GetTestData.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTestData.Api
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TestDataContext(
                serviceProvider.GetRequiredService<DbContextOptions<TestDataContext>>()))
            {
                if (context.TestItems.Any())
                {
                    return;   // DB has been seeded
                }

                context.TestItems.AddRange(
                    new TestItem
                    {
                        Name = "Jim",
                        Desc = "I'm Jim",
                        OpsTime = DateTime.Now,
                    },

                    new TestItem
                    {
                        Name = "Ducking",
                        Desc = "I'm Ducking",
                        OpsTime = DateTime.Now,
                    },

                    new TestItem
                    {
                        Name = "Bunney",
                        Desc = "I'm Bunney",
                        OpsTime = DateTime.Now,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
