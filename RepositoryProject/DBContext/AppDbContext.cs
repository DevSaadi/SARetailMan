using Microsoft.EntityFrameworkCore;
using RepositoryProject.Areas.Student.Data;
using System;

namespace RepositoryProject.DBContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Students> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__79D361B6930E16FF");

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");
            });
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__3C872F76804F2E15");

                entity.ToTable("Rols");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("PK__Users__981CF2B10C1B1086");

                entity.Property(e => e.IdUsers).HasColumnName("idUsers");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Users__idRol__1BFD2C07");
            });

            OnModelCreatingPartial(modelBuilder);


        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

        }
    }
}