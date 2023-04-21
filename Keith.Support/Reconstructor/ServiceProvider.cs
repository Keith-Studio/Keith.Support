namespace Keith.Support;

using Ninject;

public class ServiceProvider
{
    static IKernel Kernel { get; } = new StandardKernel();
    public static void LoadModule(IModule module)
    {
        Kernel.Load(new Binder(module));
    }
    public static T Get<T>() => Kernel.Get<T>();
}