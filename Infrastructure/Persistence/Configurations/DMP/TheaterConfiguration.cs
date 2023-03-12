using Domain.Entities.DMP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.DMP;
public class TheaterConfiguration : IEntityTypeConfiguration<Theater>
{
    public void Configure(EntityTypeBuilder<Theater> builder)
    {
        builder.HasKey(r => r.Id);
        builder.ToTable("DMP_Theaters");
    }
}
