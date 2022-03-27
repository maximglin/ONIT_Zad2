using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ONIT_Zad2
{
    internal class ActionCheckCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        Action action;
        Action<Exception> onExceptionAction;
        Func<bool> canExecutePredicate;
        public ActionCheckCommand(Action action, Func<bool> canExecutePredicate, Action<Exception> onExceptionAction = null)
        {
            this.action = action;
            this.onExceptionAction = onExceptionAction;
            this.canExecutePredicate = canExecutePredicate;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return canExecutePredicate?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (action != null)
                    action.Invoke();
            }
            catch (Exception ex)
            {
                onExceptionAction?.Invoke(ex);
            }
            finally
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
