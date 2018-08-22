using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetTestData.Api.Models
{
    public class TestItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public DateTime OpsTime { get; set; }
    }

    public class TestItemConfiguration : IEntityTypeConfiguration<TestItem>
    {
        public void Configure(EntityTypeBuilder<TestItem> builder)
        {
            //builder.HasKey(t => t.Id).HasName("FK_TestItem_Index");

            builder.Property(t => t.Name).IsRequired().HasMaxLength(20);

            builder.Property(t => t.Desc).HasMaxLength(256);
        }
    }
}
