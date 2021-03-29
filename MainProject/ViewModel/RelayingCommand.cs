using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject.ViewModel
{
    class RelayingCommand : ICommand
    {
        #region Fields
        readonly Predicate<object> _canExecute;
        readonly Action<object> _execute;
        #endregion //Fields



        #region Constructors
        public RelayingCommand(Action<object> action) : this(action, null) { }
        public RelayingCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute ==null)
            {
                throw new NullReferenceException("The Execute is not null");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion //Constructors



        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
        
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
        #endregion //ICommand Members
    }
}
