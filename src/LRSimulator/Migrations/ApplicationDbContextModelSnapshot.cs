using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LRSimulator.DAL;

namespace LRSimulator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LRSimulator.Models.Grade", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("CardName");

                    b.Property<int?>("MultiverseID");

                    b.Property<int>("SetReviewID");

                    b.Property<int>("Value");

                    b.HasKey("ID");

                    b.HasIndex("SetReviewID");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("LRSimulator.Models.SetReview", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("SetCode");

                    b.HasKey("ID");

                    b.ToTable("SetReviews");
                });

            modelBuilder.Entity("LRSimulator.Models.Grade", b =>
                {
                    b.HasOne("LRSimulator.Models.SetReview", "SetReview")
                        .WithMany("Grades")
                        .HasForeignKey("SetReviewID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
