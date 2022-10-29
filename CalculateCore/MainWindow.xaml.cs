using System.Windows;
using System.Windows.Controls;

namespace CalculateCore
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
           // lblResult.Content = "12341";
        }

        private void OnACButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            if(btn.Content.ToString() == "AC")
            {
                lblResult.Content = "0";
                lastNumber= 0;
                result = 0;
                return;
            }
        }
        private void OnPercentageButtonClick(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(lblResult.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber > 0)
                {
                    tempNumber *= lastNumber;
                }
                lblResult.Content = tempNumber.ToString();
            }
        }
        private void OnPointButtonClick(object sender, RoutedEventArgs e)
        {
            if(!lblResult.Content.ToString().Contains("."))
            {
                lblResult.Content = $"{lblResult.Content}.";
            }
        }
        private void OnEqualButtonClick(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(lblResult.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Substract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        if (newNumber == 0)
                        {
                            MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                   
                    default:
                        break;
                }
            }
            lblResult.Content = result.ToString();
        }
        private void OnPlusMinusButtonClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lblResult.Content.ToString(),out lastNumber))
            {
                lastNumber = lastNumber * -1;
                lblResult.Content = lastNumber.ToString();
            }
        }
        private void OnOperationButtonClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lblResult.Content.ToString(), out lastNumber))
            {
                lblResult.Content = "0";
            }
            var op = (sender as Button).Content;
            switch (op)
            {
                case "+":
                    selectedOperator = SelectedOperator.Addition;
                    break;
                case "-":
                    selectedOperator = SelectedOperator.Substraction;
                    break;
                case "*":
                    selectedOperator = SelectedOperator.Multiplication;
                    break;
                case "/":
                    selectedOperator = SelectedOperator.Division;
                    break;
                default:
                    break;
            }
        }
        private void OnNumberButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            int selectedValue = int.Parse((sender as Button).Content?.ToString());
           
            if (lblResult.Content.ToString() == "0")
            {
                lblResult.Content = selectedValue.ToString();
            }
            else
            {
                lblResult.Content = $"{lblResult.Content}{selectedValue.ToString()}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division,
    }
    public class SimpleMath
    {
        public static double Add(double n1, double n2) => n1 + n2;
        public static double Substract(double n1, double n2) => n1 - n2;
        public static double Multiply(double n1, double n2) => n1 * n2;
        public static double Divide(double n1, double n2) => n1 / n2;
        public static double Percentage(double n1, double n2) => n2==0 ? n1/100 : (n1/n2)*100;

    }
}
