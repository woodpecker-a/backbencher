using Autofac;
using STCS.Web.Models;

namespace STCS.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegisterModel>().AsSelf();
        builder.RegisterType<LoginModel>().AsSelf();

        base.Load(builder);
    }
}