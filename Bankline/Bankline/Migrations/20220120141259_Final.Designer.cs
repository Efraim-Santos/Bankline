// <auto-generated />
using System;
using Bankline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bankline.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20220120141259_Final")]
    partial class Final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Bankline.Models.BankStatementModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("StatementPeriod");

                    b.HasKey("Id");

                    b.ToTable("BankStatement");
                });

            modelBuilder.Entity("Bankline.Models.TransactionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BankStatementModelId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Type");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BankStatementModelId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Bankline.Models.TransactionModel", b =>
                {
                    b.HasOne("Bankline.Models.BankStatementModel")
                        .WithMany("Transaction")
                        .HasForeignKey("BankStatementModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
