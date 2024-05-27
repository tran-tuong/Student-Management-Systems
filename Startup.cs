using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentManagementSystems.Startup))]
namespace StudentManagementSystems
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
