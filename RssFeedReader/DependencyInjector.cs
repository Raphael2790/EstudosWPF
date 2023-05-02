using Unity;
using Unity.Lifetime;

namespace RssFeedReader;

public class DependencyInjector
{
    private static readonly UnityContainer unityContainer = new UnityContainer();

    public static void RegisterTypes<I, T>() where T : I
    {
        unityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
    }

    public static T Resolve<T>()
    {
        return unityContainer.Resolve<T>();
    }

    public static void Inject<T>(T instance)
    {
        unityContainer.RegisterInstance(instance, new ContainerControlledLifetimeManager());
    }
}
