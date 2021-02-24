using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lib.Commands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Выполнить делегат
        /// </summary>
        readonly Action<object> _execute;

        /// <summary>
        /// Проверка возможности выполнения функции
        /// </summary>
        readonly Predicate<object> _canexecute;

        public RelayCommand(Action<object> execute, Predicate<object> canexecute)
        {
            if (execute is null)
                throw new NullReferenceException("execute");

            _execute = execute;
            _canexecute = canexecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, x => true)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute is null ? false : _canexecute(parameter);
        }

        public void Execute(object parameter = null)
        {
            _execute.Invoke(parameter);
        }
    }
}
