using Loxodonta.Domain.Cards;
using Loxodonta.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loxodonta.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    internal DbSet<Card> Cards { get; set; }
    internal DbSet<Feature> Features { get; set; }
    internal DbSet<RefreshToken> RefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //RefreshToken
        modelBuilder.Entity<RefreshToken>()
            .HasKey(rt => rt.Id);

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(rt => rt.Token)
            .IsUnique();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);

        //User Role
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        //Card
        modelBuilder.Entity<Card>()
            .HasKey(card => card.Id);

        modelBuilder.Entity<Card>()
            .HasMany(card => card.Features)
            .WithOne(feature => feature.Card)
            .HasForeignKey(feature => feature.CardId);
        
        //Feature
        modelBuilder.Entity<Feature>()
            .HasKey(feature => feature.Id);
    }
}
