using Autofac;
using STCS.Web.Models;

namespace STCS.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegisterModel>().AsSelf();
        builder.RegisterType<LoginModel>().AsSelf();
        builder.RegisterType<CourseCreateModel>().AsSelf();
        builder.RegisterType<CourseEditModel>().AsSelf();
        builder.RegisterType<CourseListModel>().AsSelf();
        builder.RegisterType<StudentCreateModel>().AsSelf();
        builder.RegisterType<StudentEditModel>().AsSelf();
        builder.RegisterType<StudentListModel>().AsSelf();

        base.Load(builder);
    }
}