﻿#nullable disable

namespace Account.Data.Migrations;

[DbContext(typeof(AccountContext))]
[Migration("20220913095320_addEmailVerifictionTable")]
partial class addEmailVerifictionTable
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("Account.Data.Entities.Account", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                b.Property<int>("Balance")
                    .HasColumnType("int");

                b.Property<Guid>("CustomerID")
                    .HasColumnType("uniqueidentifier");

                b.Property<DateTime>("OpenDate")
                    .HasColumnType("datetime2");

                b.HasKey("ID");

                b.HasIndex("CustomerID");

                b.ToTable("Account");
            });

        modelBuilder.Entity("Account.Data.Entities.Customer", b =>
            {
                b.Property<Guid>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("nvarchar(40)");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnType("nvarchar(25)");

                b.HasKey("ID");

                b.HasIndex("Email")
                    .IsUnique();

                b.ToTable("Customer");
            });

        modelBuilder.Entity("Account.Data.Entities.EmailVerification", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnType("nvarchar(40)");

                b.Property<DateTime>("ExpirationTime")
                    .HasColumnType("datetime2");

                b.Property<int>("VerificationCode")
                    .HasColumnType("int");

                b.HasKey("ID");

                b.ToTable("EmailVerification");
            });

        modelBuilder.Entity("Account.Data.Entities.OperationsHistory", b =>
            {
                b.Property<int>("ID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                b.Property<int>("AccountID")
                    .HasColumnType("int");

                b.Property<int>("Balance")
                    .HasColumnType("int");

                b.Property<bool>("Debit")
                    .HasColumnType("bit");

                b.Property<DateTime>("OperationTime")
                    .HasColumnType("datetime2");

                b.Property<int>("TransactionAmount")
                    .HasColumnType("int");

                b.Property<int>("TransactionID")
                    .HasColumnType("int");

                b.HasKey("ID");

                b.HasIndex("AccountID");

                b.ToTable("OperationsHistory");
            });

        modelBuilder.Entity("Account.Data.Entities.Account", b =>
            {
                b.HasOne("Account.Data.Entities.Customer", "Customer")
                    .WithMany()
                    .HasForeignKey("CustomerID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Customer");
            });

        modelBuilder.Entity("Account.Data.Entities.OperationsHistory", b =>
            {
                b.HasOne("Account.Data.Entities.Account", "Account")
                    .WithMany()
                    .HasForeignKey("AccountID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Account");
            });
#pragma warning restore 612, 618
    }
}
