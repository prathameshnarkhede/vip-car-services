using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Commands
        public ICommand SubmitCommand { get; set; }

        #endregion

        #region Constructor
        public ViewModelBase()
        {
            SubmitCommand = new RelayCommand(Submit);
        }
        #endregion

        #region Navigation
        public abstract void Submit(object obj);
        #endregion

        #region EventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private readonly bool _canExecute;

        public RelayCommand(Action<object> execute, bool canExecute = true)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
