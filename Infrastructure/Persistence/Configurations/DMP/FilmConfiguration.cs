using Domain.Entities.DMP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.DMP;
public class FilmConfiguration : IEntityTypeConfiguration<Films>
{
    public void Configure(EntityTypeBuilder<Films> builder)
    {
        builder.HasKey(r => r.Id);
        builder.ToTable("DMP_Films");
    }
}
