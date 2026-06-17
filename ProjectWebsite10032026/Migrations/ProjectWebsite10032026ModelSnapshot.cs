using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProjectWebsite10032026.Data;

namespace ProjectWebsite10032026.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class ProjectWebsite10032026ModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("ProjectWebsite10032026.Models.Download", b =>
            {
                b.Property<int>("ID").ValueGeneratedOnAdd();
                b.Property<string>("Description").IsRequired(false);
                b.Property<string>("FileName").IsRequired(false);
                b.Property<string>("FilePath").IsRequired(false);
                b.HasKey("ID");
                b.ToTable("Downloads");
            });

            modelBuilder.Entity("ProjectWebsite10032026.Models.Link", b =>
            {
                b.Property<int>("ID").ValueGeneratedOnAdd();
                b.Property<string>("Category").IsRequired(false);
                b.Property<string>("Description").IsRequired(false);
                b.Property<string>("Url").IsRequired(false);
                b.HasKey("ID");
                b.ToTable("Links");
            });
        }
    }
}
