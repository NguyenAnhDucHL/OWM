using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OMW_Project.Models
{
    public class ProjectDbContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<CategoryPost> CategoryPosts { get; set; }
        public DbSet<ConsultResult> ConsultResults { get; set; }
        public DbSet<Consulting> Consultings { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<DoctorContribution> DoctorContributions { get; set; }
        public DbSet<DoctorPayment> DoctorPayments { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProductSuggest> ProductSuggests { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public ProjectDbContext()
            : base("AspNetIdentityConnection", throwIfV1Schema: false)
        {
        }

        static ProjectDbContext()
        {
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ProjectDbContext>(new ProjectDbInitializer());
        }

        public static ProjectDbContext Create()
        {
            return new ProjectDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
        }
    }
}