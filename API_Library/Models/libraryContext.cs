using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API_Library.Models
{
    public partial class libraryContext : DbContext
    {
        public libraryContext()
        {
        }

        public libraryContext(DbContextOptions<libraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Borrowing> Borrowings { get; set; }
        public virtual DbSet<BorrowingDetail> BorrowingDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Copy> Copies { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=greenlibrary.mssql.somee.com;packet size=4096;user id=hoangquynh_SQLLogin_1;pwd=123456789;data source=greenlibrary.mssql.somee.com;persist security info=False;initial catalog=greenlibrary");
                //optionsBuilder.UseSqlServer("Server=DESKTOP-RGS773K\\HOANGQUYNH;Database=library;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Account_Staff");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).ValueGeneratedNever();

                entity.Property(e => e.Author).HasMaxLength(250);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Publisher).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Book_Category");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Book_Language");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Book_Position");
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("Borrower");

                entity.Property(e => e.BorrowerId)
                    .ValueGeneratedNever()
                    .HasColumnName("BorrowerID");

                entity.Property(e => e.AccountBalance).HasColumnName("account_balance");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.StartDay).HasColumnType("date");
            });

            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.ToTable("Borrowing");

                entity.Property(e => e.BorrowingId).ValueGeneratedNever();

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("date")
                    .HasColumnName("Appointment_date");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("date")
                    .HasColumnName("Borrowed_date");

                entity.Property(e => e.BorrowerId).HasColumnName("BorrowerID");

                entity.Property(e => e.NotificationStatus).HasColumnName("Notification_status");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.BorrowerId)
                    .HasConstraintName("FK_Borrowing_Borrower");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Borrowing_Staff");
            });

            modelBuilder.Entity<BorrowingDetail>(entity =>
            {
                entity.ToTable("Borrowing_detail");

                entity.Property(e => e.BorrowingDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("Borrowing_detailId");

                entity.Property(e => e.BorrowStatus).HasColumnName("borrow_status");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Durability).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("Return_date");

                entity.HasOne(d => d.Borrowing)
                    .WithMany(p => p.BorrowingDetails)
                    .HasForeignKey(d => d.BorrowingId)
                    .HasConstraintName("FK_Borrowing_detail_Book");

                entity.HasOne(d => d.Copy)
                    .WithMany(p => p.BorrowingDetails)
                    .HasForeignKey(d => d.CopyId)
                    .HasConstraintName("FK_Borrowing_detail_Copy");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Copy>(entity =>
            {
                entity.ToTable("Copy");

                entity.Property(e => e.CopyId).ValueGeneratedNever();

                entity.Property(e => e.BorrowStatus).HasColumnName("borrow_status");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Durability).HasColumnType("decimal(4, 1)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Copies)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Copy_Book");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).ValueGeneratedNever();

                entity.Property(e => e.Path)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Image_Book");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.LanguageId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.LoginId).ValueGeneratedNever();

                entity.Property(e => e.LoginDate)
                    .HasColumnType("date")
                    .HasColumnName("Login_date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Login_Account");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.PositionId).ValueGeneratedNever();
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__Staff__96D4AB17C638E119");

                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.StartDay).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
