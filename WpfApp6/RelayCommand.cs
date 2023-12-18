using System;
using System.Windows.Input;

namespace Calculator.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(_ => execute(), _ => canExecute?.Invoke() ?? true)
        {
        }
    }
}
