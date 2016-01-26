using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodDonationWebApp.Startup))]
namespace BloodDonationWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
