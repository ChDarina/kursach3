using kursach3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using kursach3.ViewModels;

namespace kursach3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RolePlay> RolePlays { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<SocialMediaUser> SocialMediaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RolePlay>().ToTable("RolePlays");
            builder
                .Entity<RolePlay>()
                .Property(s => s.Name).IsRequired().HasMaxLength(500);
            builder
                .Entity<RolePlay>()
                .HasOne(s => s.Master)
                .WithMany(t => t.RolePlays)
                .HasForeignKey(s => s.MasterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Feed>().ToTable("Feed");
            builder
                .Entity<Feed>()
                .HasOne(s => s.User)
                .WithMany(u => u.UserFeed)
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            builder.Entity<Room>().ToTable("Rooms");
            builder
                .Entity<Room>()
                .Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder
                .Entity<Room>()
                .HasOne(s => s.RolePlay)
                .WithMany(u => u.Rooms)
                .HasForeignKey(s => s.RolePlayId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>().ToTable("Messages");
            builder
                .Entity<Message>()
                .Property(s => s.Content).IsRequired().HasMaxLength(500);
            builder
                .Entity<Message>()
                .HasOne(s => s.ToRoom)
                .WithMany(m => m.Messages)
                .HasForeignKey(s => s.ToRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Feed>().ToTable("Feed");

            builder.Entity<Friend>().ToTable("Friends");
            builder.Entity<Friend>().HasKey(t => new { t.UserId, t.UserFriendId });
            builder
                .Entity<Friend>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .Entity<Friend>()
                .HasOne(e => e.UserFriend)
                .WithMany()
                .HasForeignKey(s => s.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Character>().ToTable("Characters");
            builder.Entity<Character>().HasKey(t => new { t.UserId, t.RolePlayId });
            builder
                .Entity<Character>()
                .HasOne(e => e.User)
                .WithMany(t => t.Characters)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Entity<Character>()
                .HasOne(e => e.RolePlay)
                .WithMany(t => t.Characters)
                .HasForeignKey(s => s.RolePlayId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            builder.Entity<SocialMedia>().ToTable("SocialMedias");

            builder.Entity<SocialMediaUser>().ToTable("SocialMediaUsers");
            builder.Entity<SocialMediaUser>().HasKey(t => new { t.UserId, t.SocialMediaId });
            builder
                .Entity<SocialMediaUser>()
                .HasOne(e => e.User)
                .WithMany(t => t.SocialMediaUsers)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .Entity<SocialMediaUser>()
                .HasOne(e => e.SocialMedia)
                .WithMany(t => t.SocialMediaUsers)
                .HasForeignKey(s => s.SocialMediaId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}