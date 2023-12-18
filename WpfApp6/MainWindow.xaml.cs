using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string currentOperator = "";
        private double? first_num = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Content;

            if (currentInput == "0" || currentInput == "-0")
            {
                currentInput = number;
            }
            else
            {
                currentInput += number;
            }

            UpdateDisplay();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(currentInput, out double result))
            {
                if (first_num != null)
                {
                    Equals_Click(sender, e);
                }

                currentOperator = (string)((Button)sender).Content;
                first_num = result;
                currentInput = "0";
            }
            else
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (first_num != null)
            {
                double sec_num;
                if (double.TryParse(currentInput, out sec_num))
                {
                    switch (currentOperator)
                    {
                        case "+":
                            currentInput = (first_num + sec_num).ToString();
                            break;
                        case "-":
                            currentInput = (first_num - sec_num).ToString();
                            break;
                        case "*":
                            currentInput = (first_num * sec_num).ToString();
                            break;
                        case "/":
                            if (sec_num != 0)
                            {
                                currentInput = (first_num / sec_num).ToString();
                            }
                            else
                            {
                                currentInput = "Error";
                            }
                            break;
                    }

                    first_num = double.Parse(currentInput);
                    currentOperator = "";

                    UpdateDisplay();
                }
                else
                {
                    currentInput = "Error";
                    UpdateDisplay();
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.Length > 1)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }
            else
            {
                currentInput = "0";
            }

            UpdateDisplay();
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            currentOperator = "";
            first_num = null;

            UpdateDisplay();
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains(","))
            {
                currentInput += ",";
                UpdateDisplay();
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.Length > 1)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                UpdateDisplay();
            }
            else
            {
                currentInput = "0";
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            Display.Text = currentInput;
        }
    }
}
