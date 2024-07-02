using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthInsurance.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactPage> ContactPages { get; set; }

    public virtual DbSet<HomePage> HomePages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##Project;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##PROJECT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008573");

            entity.ToTable("ABOUT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Background)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("BACKGROUND");
            entity.Property(e => e.Img)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG");
            entity.Property(e => e.Img1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG1");
            entity.Property(e => e.Img2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG2");
            entity.Property(e => e.Img3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG3");
            entity.Property(e => e.Info1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO1");
            entity.Property(e => e.Info2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO2");
            entity.Property(e => e.Info3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO3");
            entity.Property(e => e.Txt1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TXT1");
            entity.Property(e => e.Txt2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TXT2");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008613");

            entity.ToTable("BANK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardnumber)
                .HasColumnType("NUMBER")
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Cvv)
                .HasColumnType("NUMBER")
                .HasColumnName("CVV");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008565");

            entity.ToTable("BENEFICIARIES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Proofimage)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("PROOFIMAGE");
            entity.Property(e => e.Relative)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RELATIVE");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.Subscriptionid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTIONID");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Beneficiaries)
                .HasForeignKey(d => d.Subscriptionid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("SYS_C008566");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008575");

            entity.ToTable("CONTACT");

            entity.HasIndex(e => e.Email, "SYS_C008576").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<ContactPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008599");

            entity.ToTable("CONTACT_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Background)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("BACKGROUND");
            entity.Property(e => e.Text1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TEXT1");
            entity.Property(e => e.Text2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TEXT2");
            entity.Property(e => e.Text3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TEXT3");
        });

        modelBuilder.Entity<HomePage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008571");

            entity.ToTable("HOME_PAGE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Background)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("BACKGROUND");
            entity.Property(e => e.Footerabout)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FOOTERABOUT");
            entity.Property(e => e.Footercontact)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FOOTERCONTACT");
            entity.Property(e => e.Footerlink)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("FOOTERLINK");
            entity.Property(e => e.Img)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG");
            entity.Property(e => e.Img1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG1");
            entity.Property(e => e.Img2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG2");
            entity.Property(e => e.Img3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMG3");
            entity.Property(e => e.Info1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO1");
            entity.Property(e => e.Info2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO2");
            entity.Property(e => e.Info3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("INFO3");
            entity.Property(e => e.Item1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ITEM1");
            entity.Property(e => e.Item2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ITEM2");
            entity.Property(e => e.Item3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ITEM3");
            entity.Property(e => e.Logo)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("LOGO");
            entity.Property(e => e.Txt1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TXT1");
            entity.Property(e => e.Txt2)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TXT2");
            entity.Property(e => e.Txt3)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("TXT3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008579");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Role1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ROLE");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008562");

            entity.ToTable("SUBSCRIPTIONS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Subscriptiondate)
                .HasColumnType("DATE")
                .HasColumnName("SUBSCRIPTIONDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008614");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008568");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Status)
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008569");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008559");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "SYS_C008560").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Issub)
                .HasColumnType("NUMBER")
                .HasColumnName("ISSUB");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008581");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
