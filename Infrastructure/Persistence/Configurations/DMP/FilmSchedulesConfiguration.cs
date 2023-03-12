using Domain.Entities.DMP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.DMP;
public class FilmSchedulesConfiguration : IEntityTypeConfiguration<FilmSchedules>
{
    public void Configure(EntityTypeBuilder<FilmSchedules> builder)
    {
        builder.HasKey(r => r.Id);
        builder.ToTable("DMP_FilmSchedules");
    }
}
