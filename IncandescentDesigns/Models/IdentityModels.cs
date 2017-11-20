using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncandescentDesigns.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class AccountDisabled
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
    }

    public class ApplicationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(IncandescentDesigns.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.SaveChanges();
        }

        bool AddUserAndRole(IncandescentDesigns.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin@incandescentdesigns.com",
                Email = "admin@incandescentdesigns.com",
                EmailConfirmed = true,
            };
            ir = um.Create(user, "1q2w3e$R%T^Y");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "admin");

            return ir.Succeeded;
        }

        //bool AddUserAndRoleTwo(IncandescentDesigns.Models.ApplicationDbContext context)
        //{
        //    IdentityResult ir;
        //    var rm = new RoleManager<IdentityRole>
        //        (new RoleStore<IdentityRole>(context));
        //    ir = rm.Create(new IdentityRole("user"));
        //    var um = new UserManager<ApplicationUser>(
        //        new UserStore<ApplicationUser>(context));
        //    var user = new ApplicationUser()
        //    {
        //        UserName = "jgersztyn13@wou.edu",
        //        Email = "jgersztyn13@wou.edu",
        //        EmailConfirmed = true,
        //    };
        //    ir = um.Create(user, "1q2w#E$R");
        //    if (ir.Succeeded == false)
        //        return ir.Succeeded;
        //    ir = um.AddToRole(user.Id, "user");
        //    return ir.Succeeded;
        //}
    }
}