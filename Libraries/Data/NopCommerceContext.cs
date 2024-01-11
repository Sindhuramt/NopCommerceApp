using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Libraries.Data
{
    public partial class NopCommerceContext : DbContext
    {
        public NopCommerceContext()
        {
        }

        public NopCommerceContext(DbContextOptions<NopCommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCartItem> TblCartItem { get; set; }
        public virtual DbSet<TblCartMaster> TblCartMaster { get; set; }
        public virtual DbSet<TblCustomerMaster> TblCustomerMaster { get; set; }
        public virtual DbSet<TblOrderMaster> TblOrderMaster { get; set; }
        public virtual DbSet<TblProductMaster> TblProductMaster { get; set; }
        public virtual DbSet<TblSecurityQuestionMaster> TblSecurityQuestionMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["NopCommerceContext"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCartItem>(entity =>
            {
                entity.ToTable("tbl_cart_item");

                entity.Property(e => e.CartmasterId)
                    .HasColumnName("CartmasterID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblCartMaster>(entity =>
            {
                entity.ToTable("tbl_cart_master");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsProcessed).HasDefaultValueSql("((0))");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblCustomerMaster>(entity =>
            {
                entity.ToTable("tbl_customer_master");

                entity.HasIndex(e => e.AccountNo)
                    .HasName("UQ__tbl_cust__46A52637A538291C")
                    .IsUnique();

                entity.Property(e => e.AccountNo)
                    .HasColumnName("account_no")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerAddress)
                    .HasColumnName("customer_address")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerEmail)
                    .HasColumnName("customer_email")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerFname)
                    .HasColumnName("customer_fname")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerLname)
                    .HasColumnName("customer_lname")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerMobile)
                    .HasColumnName("customer_mobile")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsBlocked)
                    .HasColumnName("is_blocked")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LoginPassword)
                    .HasColumnName("login_password")
                    .HasMaxLength(100);

                entity.Property(e => e.SecurityAnswerCode)
                    .HasColumnName("security_answer_code")
                    .HasMaxLength(100);

                entity.Property(e => e.SecurityQuestionsCode)
                    .HasColumnName("security_questions_Code")
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblOrderMaster>(entity =>
            {
                entity.ToTable("tbl_order_master");

                entity.HasIndex(e => e.TransactionId)
                    .HasName("UQ__tbl_orde__55433A6AAF3F72CE")
                    .IsUnique();

                entity.Property(e => e.CartMasterId)
                    .HasColumnName("CartMasterID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(day,(5),getdate()))");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentMode).HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Initiated')");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionId).HasMaxLength(200);
            });

            modelBuilder.Entity<TblProductMaster>(entity =>
            {
                entity.ToTable("tbl_product_master");

                entity.Property(e => e.ProductDescription).HasMaxLength(500);

                entity.Property(e => e.ProductImage).HasMaxLength(100);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductQuantity).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblSecurityQuestionMaster>(entity =>
            {
                entity.ToTable("tbl_security_question_master");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
