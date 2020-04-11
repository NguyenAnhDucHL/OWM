using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web;

namespace OMW_Project.Models
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ProjectDbInitializer : DropCreateDatabaseIfModelChanges<ProjectDbContext>
    {
        protected override void Seed(ProjectDbContext context)
        {
            InitializeIdentity(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentity(ProjectDbContext db)
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@abc.com";
            const string password = "Abc_123";
            string[] roleName = new string[] { "Admin","User","Doctor","WebManager"};
            //Create Role Admin if it does not exist
            foreach (var item in roleName)
            {
                var role = roleManager.FindByName(item);
                if (role == null)
                {
                    role = new IdentityRole(item);
                    var roleResult = roleManager.Create(role);
                }
            }


            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(roleName[0]))
            {
                var result = userManager.AddToRole(user.Id, roleName[0]);
            }
        }
    }
}
