namespace Keith.Support;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public class NotifyPropertyChanged : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        RaisePropertyChanged(propertyName);
    }
    protected bool Set<T>(ref T field, T Value, [CallerMemberName] string PropertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, Value))
            return false;
        field = Value;
        RaisePropertyChanged(PropertyName);
        return true;
    }
    protected void RaiseAllChanged()
    {
        RaisePropertyChanged("");
    }
    protected object mPropertyValueCheckLock = new object();
    protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
    {
        lock (mPropertyValueCheckLock)
        {
            if (updatingFlag.GetPropertyValue())
                return;
            updatingFlag.SetPropertyValue(true);
        }
        try
        {
            await action();
        }
        finally
        {
            updatingFlag.SetPropertyValue(false);
        }
    }
}