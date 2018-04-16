using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Configuration;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public UserLogin() : base() { }
    }

    public class UserRole : IdentityUserRole<int>
    {
        public UserRole() : base() { }
    }

    public class UserClaim : IdentityUserClaim<int>
    {
        public UserClaim() : base() { }
    }

    public class User : IdentityUser <int, UserLogin, UserRole, UserClaim>
    {
         public User() : base() { }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Patronymic { get; set; }
    }
    /// <summary>
    /// Тип роли доступа
    /// </summary>
    public class Role : IdentityRole<int, UserRole>
    {
        public string Label { get; set; }

        public string Description { get; set; }

        public Role() : base() { }

        public Role(string roleName) : this(roleName, null) { }

        public Role(string roleName, string roleLabel)
            : base()
        {
            Name = roleName;
            Label = roleLabel;
        }
    }
    public class RoleManager : RoleManager<Role, int>
    {
        public RoleManager(RoleStore store)
            : base(store)
        {
        }

        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context)
        {
            return new RoleManager(new RoleStore(context.Get<ApplicationDbContext>()));
        }
    }
    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(DbContext context) : base(context) { }
    }
    /// <summary>
    /// Тип хранилища сущностей Identity
    /// </summary>
    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DbContext context) : base(context) { }
    }
    public class UserManager : UserManager<User, int>
    {
        public UserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            UserManager um = new UserManager(new UserStore(db));
            um.UserValidator = new UserValidator<User, int>(um) { AllowOnlyAlphanumericUserNames = false, RequireUniqueEmail = true };
            return um;
        }
    }

    public class IdentityDbInit : NullDatabaseInitializer<ApplicationDbContext> { }//закоментируй, запусти, раскомментируй

    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new IdentityDbInit());
        }

        public static ApplicationDbContext Create(string connStringName)
        {
            return new ApplicationDbContext(connStringName);
        }

        public ApplicationDbContext() : base(ConfigurationManager.AppSettings["sys:connectionName"]) { }

        public ApplicationDbContext(string connStringName)
            : base(connStringName)
        {
        }

    }
    //public class IdentityDbInit : CreateDatabaseIfNotExists<ApplicationDbContext> { }  //раскомментируй, запусти, закомментируй
}