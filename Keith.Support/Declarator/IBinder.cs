namespace Keith.Support;

public interface IBinder
{
    void BindSingleton<T>();
    void Bind<TFrom, TTarget>(bool Singleton = true) where TTarget : TFrom;
    T Get<T>();
}