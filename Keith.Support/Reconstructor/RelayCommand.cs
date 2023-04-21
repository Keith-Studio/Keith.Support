namespace Keith.Support;

using System;
using System.Windows.Input;

public class RelayCommand<T> : ICommand
{
    private Action<T> mExecute;
    private Func<T, bool> mCanExecute;
    public RelayCommand(Action<T> execute, bool keepTargetAlive = false)
    { 
        this.mExecute = execute;
    }
    public RelayCommand(Action<T> execute, Func<T, bool> canExecute, bool keepTargetAlive = false)
    {
        this.mExecute = execute;
        this.mCanExecute = canExecute;
    }
    public event EventHandler CanExecuteChanged = (sender, e) => { };
    public bool CanExecute(object parameter)
    {
        if (mCanExecute != null)
            return mCanExecute.Invoke((T)parameter);
        else
            return true;
    }
    public virtual void Execute(object parameter)
    {
        mExecute.Invoke((T)parameter);
    }
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged(this, null);
    }
}
public class RelayCommand : ICommand
{
    private Action mExecute;
    private Func<bool> mCanExecute;
    public RelayCommand(Action execute, bool keepTargetAlive = false)
    {
        this.mExecute = execute;
    }
    public RelayCommand(Action execute, Func<bool> canExecute, bool keepTargetAlive = false)
    {
        this.mExecute = execute;
        this.mCanExecute = canExecute;
    }
    public event EventHandler CanExecuteChanged = (sender, e) => { };
    public bool CanExecute(object parameter)
    {
        if (mCanExecute != null)
            return this.mCanExecute.Invoke();
        else
            return true;
    }
    public virtual void Execute(object parameter)
    {
        this.mExecute.Invoke();
    }
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged.Invoke(this, null);
    }
}