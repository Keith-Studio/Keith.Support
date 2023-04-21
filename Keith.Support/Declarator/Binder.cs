namespace Keith.Support;

using Ninject.Modules;
public class Binder : NinjectModule, IBinder
{
    private readonly IModule module;
    public Binder(IModule module)
    {
        this.module = module;
    }
    public void BindSingleton<T>()
    {
        Bind<T>().ToSelf().InSingletonScope();
    }
    public void Bind<TFrom, TTarget>(bool singleton = true) where TTarget : TFrom
    {
        var binding = Bind<TFrom>().To<TTarget>();
        if (singleton)
            binding.InSingletonScope();
    }
    public T Get<T>() => ServiceProvider.Get<T>();
    public override void Load()
    {
        module.OnLoad(this);
    }
}