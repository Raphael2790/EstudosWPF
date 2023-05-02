using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculadoraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            percentageButton.Click += PercentageButton_Click;
            negativeButton.Click += NegativeButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addiction:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = lastNumber * -1;
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double tempNumber))
            {
                tempNumber /= 100;
                if (lastNumber is not default(double))
                    tempNumber = lastNumber * tempNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = 0;
            result = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
                selectedOperator = (sender as Button)?.Content?.ToString() switch
                {
                    "+" => SelectedOperator.Addiction,
                    "-" => SelectedOperator.Subtraction,
                    "/" => SelectedOperator.Division,
                    "*" => SelectedOperator.Multiplication,
                    _ => throw new NotImplementedException()
                };
            }
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString()!.Contains('.'))
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            if(sender is Button button)
            {
                _ = int.TryParse(button?.Content?.ToString(), out selectedValue); 
            }

            if(resultLabel.Content.ToString() is "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addiction,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double number1, double number2)
            => number1 + number2;

        public static double Subtract(double number1, double number2)
            => number1 - number2;

        public static double Multiply(double number1, double number2)
            => number1 * number2;

        public static double Divide(double number1, double number2)
        {
            if(number2 is default(double))
            {
                MessageBox.Show("Division by 0 is not permited", "Wrong division", MessageBoxButton.OK, MessageBoxImage.Warning);
                return default;
            }

            return number1 / number2;
        }
    }
}
