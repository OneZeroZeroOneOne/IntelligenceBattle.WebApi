using IntelligenceBattle.WebApi.Dal.Models;
using Microsoft.EntityFrameworkCore;


namespace IntelligenceBattle.WebApi.Dal.Contexts
{
    public partial class PublicContext : DbContext
    {
        public string _connString;
        public PublicContext(string connString)
        {
            _connString = connString;
        }

        public PublicContext(DbContextOptions<PublicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AuthorizationCenter> AuthorizationCenters { get; set; }
        public virtual DbSet<AuthorizationProvider> AuthorizationProviders { get; set; }
        public virtual DbSet<AuthorizationProviderType> AuthorizationProviderTypes { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameUser> GameUsers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
        public virtual DbSet<UserSecurity> UserSecurities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this._connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Text).HasColumnType("character varying");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("Answer_QuestionId_fkey");
            });

            modelBuilder.Entity<AuthorizationCenter>(entity =>
            {
                entity.ToTable("AuthorizationCenter");

                entity.Property(e => e.Title).HasColumnType("character varying");
            });

            modelBuilder.Entity<AuthorizationProvider>(entity =>
            {
                entity.ToTable("AuthorizationProvider");

                entity.Property(e => e.Key).HasColumnType("character varying");

                entity.HasOne(d => d.AuthorizationProviderType)
                    .WithMany(p => p.AuthorizationProviders)
                    .HasForeignKey(d => d.AuthorizationProviderTypeId)
                    .HasConstraintName("AuthorizationProvider_AuthorizationProviderTypeId_fkey");
            });

            modelBuilder.Entity<AuthorizationProviderType>(entity =>
            {
                entity.ToTable("AuthorizationProviderType");

                entity.Property(e => e.Title).HasColumnType("character varying");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");
            });

            modelBuilder.Entity<GameUser>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.UserId })
                    .HasName("GameUser_pkey");

                entity.ToTable("GameUser");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameUsers)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GameUser_GameId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GameUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GameUser_UserId_fkey");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Text).HasColumnType("character varying");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Surname).HasColumnType("character varying");
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.ToTable("UserAnswer");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("UserAnswer_AnswerId_fkey");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("UserAnswer_GameId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserAnswer_UserId_fkey");
            });

            modelBuilder.Entity<UserSecurity>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RealId })
                    .HasName("UserSecurity_pkey");

                entity.ToTable("UserSecurity");

                entity.Property(e => e.Login).HasColumnType("character varying");

                entity.Property(e => e.Password).HasColumnType("character varying");

                entity.HasOne(d => d.AuthorizationCenter)
                    .WithMany(p => p.UserSecurities)
                    .HasForeignKey(d => d.AuthorizationCenterId)
                    .HasConstraintName("UserSecurity_AuthorizationCenterId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSecurities)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserSecurity_UserId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
