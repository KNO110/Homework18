using System;
using System.Windows.Input;
using Calculator.Commands;
using System.ComponentModel;

namespace Calculator
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CalculatorViewModel : ViewModelBase
    {
        private string _currentInput = "0";
        private string _currentOperator = "";
        private double? _firstNum = null;

        public string CurInput
        {
            get => _currentInput;
            set
            {
                _currentInput = value;
                OnPropertyChanged(nameof(CurInput));
            }
        }

        public ICommand NumberCommand => new RelayCommand<string>(NumClick);
        public ICommand OperatorCommand => new RelayCommand<string>(OperatorClick);
        public ICommand EqualsCommand => new RelayCommand(EqClick);
        public ICommand ClearCommand => new RelayCommand(ClearClick);
        public ICommand ClearAllCommand => new RelayCommand(ClearAllClick);
        public ICommand DotCommand => new RelayCommand(DotClick);
        public ICommand BackspaceCommand => new RelayCommand(BackspaceClick);

        private void NumClick(string number)
        {
            if (_currentInput == "0" || _currentInput == "-0")
            {
                _currentInput = number;
            }
            else
            {
                _currentInput += number;
            }

            UpdateDisplay();
        }

        private void OperatorClick(string op)
        {
            if (double.TryParse(_currentInput, out double result))
            {
                if (_firstNum != null)
                {
                    EqClick();
                }

                _currentOperator = op;
                _firstNum = result;
                _currentInput = "0";
            }
            else
            {
                _currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void EqClick()
        {
            if (_firstNum != null)
            {
                double secNum;
                if (double.TryParse(_currentInput, out secNum))
                {
                    switch (_currentOperator)
                    {
                        case "+":
                            _currentInput = (_firstNum + secNum).ToString();
                            break;
                        case "-":
                            _currentInput = (_firstNum - secNum).ToString();
                            break;
                        case "*":
                            _currentInput = (_firstNum * secNum).ToString();
                            break;
                        case "/":
                            if (secNum != 0)
                            {
                                _currentInput = (_firstNum / secNum).ToString();
                            }
                            else
                            {
                                _currentInput = "Error";
                            }
                            break;
                    }

                    _firstNum = double.Parse(_currentInput);
                    _currentOperator = "";

                    UpdateDisplay();
                }
                else
                {
                    _currentInput = "Error";
                    UpdateDisplay();
                }
            }
        }

        private void ClearClick()
        {
            if (_currentInput.Length > 1)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            }
            else
            {
                _currentInput = "0";
            }

            UpdateDisplay();
        }

        private void ClearAllClick()
        {
            _currentInput = "0";
            _currentOperator = "";
            _firstNum = null;

            UpdateDisplay();
        }

        private void DotClick()
        {
            if (!_currentInput.Contains("."))
            {
                _currentInput += ".";
                UpdateDisplay();
            }
        }

        private void BackspaceClick()
        {
            if (_currentInput.Length > 1)
            {
                _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                UpdateDisplay();
            }
            else
            {
                _currentInput = "0";
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged(nameof(CurInput));
        }
    }
}
