using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;
public class IdAutoBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime UpdDateTime { get; set; }

    public DateTime InsDateTime { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}

public class IdAutoBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : IdAutoBase
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(u => u.RowVersion)
               .IsRowVersion();
    }
}