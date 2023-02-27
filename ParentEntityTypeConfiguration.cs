using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorWasmEfCoreNormalized;

public class ParentEntityTypeConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder
            .Property(parent => parent.Children)
            .HasConversion(
                t => JsonSerializer.Serialize(t, (JsonSerializerOptions)default!),
                t => JsonSerializer.Deserialize<List<Child>>(t, (JsonSerializerOptions)default!)!,
                new ValueComparer<List<Child>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }    
}