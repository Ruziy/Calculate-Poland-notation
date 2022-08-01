using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;

namespace первый_код_на_wpf
{
    /// <summary>
    /// Логика взаимодействия для simplepath.xaml
    /// </summary>
    public partial class simplepath : Window
    {
        public simplepath()
        {
            InitializeComponent();
            textLabel.Text = "0";
            foreach (UIElement el in MainRoot.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool IsDelimeter(char c)
            {
                if ((" =".IndexOf(c) != -1))
                    return true;
                return false;
            }
            bool IsOperator(char с)
            {
                if (("+-/*^()".IndexOf(с) != -1))
                    return true;
                return false;
            }
            byte GetPriority(char s)
            {
                switch (s)
                {
                    case '(': return 0;
                    case ')': return 1;
                    case '+': return 2;
                    case '-': return 3;
                    case '*': return 4;
                    case '/': return 4;
                    case '^': return 5;
                    default: return 6;
                }
            }
            string GetExpression(string input)
            {
                string output = string.Empty; //Строка для хранения выражения
                Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

                for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
                {
                    //Разделители пропускаем
                    if (IsDelimeter(input[i]))
                        continue; //Переходим к следующему символу

                    //Если символ - цифра, то считываем все число
                    if (Char.IsDigit(input[i])) //Если цифра
                    {
                        //Читаем до разделителя или оператора, что бы получить число
                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            output += input[i]; //Добавляем каждую цифру числа к нашей строке
                            i++; //Переходим к следующему символу

                            if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                        }

                        output += " "; //Дописываем после числа пробел в строку с выражением
                        i--; //Возвращаемся на один символ назад, к символу перед разделителем
                    }

                    //Если символ - оператор
                    if (IsOperator(input[i])) //Если оператор
                    {
                        if (input[i] == '(') //Если символ - открывающая скобка
                            operStack.Push(input[i]); //Записываем её в стек
                        else if (input[i] == ')') //Если символ - закрывающая скобка
                        {
                            //Выписываем все операторы до открывающей скобки в строку

                            try
                            {
                                char s = operStack.Pop();


                                while (s != '(')
                                {
                                    output += s.ToString() + ' ';
                                    s = operStack.Pop();
                                }
                            }
                            catch (System.InvalidOperationException)
                            {

                                textLabel.Text = "ERROR";
                            }
                        }
                        else //Если любой другой оператор
                        {
                            if (operStack.Count > 0) //Если в стеке есть элементы
                                if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                    output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                            operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                        }
                    }
                }

                //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
                while (operStack.Count > 0)
                    output += operStack.Pop() + " ";

                return output; //Возвращаем выражение в постфиксной записи
            }
            double Counting(string input)
            {
                double result = 0; //Результат
                Stack<double> temp = new Stack<double>(); //Dhtvtyysq стек для решения

                for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
                {
                    //Если символ - цифра, то читаем все число и записываем на вершину стека
                    if (Char.IsDigit(input[i]))
                    {
                        string a = string.Empty;

                        while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                        {
                            a += input[i]; //Добавляем
                            i++;
                            if (i == input.Length) break;
                        }
                        temp.Push(double.Parse(a)); //Записываем в стек
                        i--;
                    }
                    else if (IsOperator(input[i])) //Если символ - оператор
                    {
                        //Берем два последних значения из стека

                        try
                        {
                            double a = temp.Pop();


                            double b = temp.Pop();



                            switch (input[i]) //И производим над ними действие, согласно оператору
                            {
                                case '+': result = b + a; break;
                                case '-': result = b - a; break;
                                case '*': result = b * a; break;
                                case '/': result = b / a; break;
                                case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                            }
                        }
                        catch (System.InvalidOperationException)
                        {

                            textLabel.Text = "ERROR";
                        }
                        temp.Push(result); //Результат вычисления записываем обратно в стек
                    }
                }
                return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
            }
            double Calculate(string input)
            {
                string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
                double result = Counting(output); //Решаем полученное выражение
                return result; //Возвращаем результат
            }
            int Check_1(string stroka, string find)//ПРОВЕРКА
            {
                var result = 0;
                var j = 0; // Позиция в подстроке
                for (int i = 0; i < stroka.Length; i++)
                {
                    if (find[j] == stroka[i])
                    {
                        j++;
                        if (j == find.Length)
                        {
                            result++;
                            j = 0;
                        }
                    }


                }
                return result;
            }

            try
            {
                if (textLabel.Text.Contains("--"))
                {
                    int a = textLabel.Text.IndexOf("--");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("++"))
                {
                    int a = textLabel.Text.IndexOf("++");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("**"))
                {
                    int a = textLabel.Text.IndexOf("**");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("//"))
                {
                    int a = textLabel.Text.IndexOf("//");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("+-"))
                {
                    int a = textLabel.Text.IndexOf("+-");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("-+"))
                {
                    int a = textLabel.Text.IndexOf("-+");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("*+"))
                {
                    int a = textLabel.Text.IndexOf("*+");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("+*"))
                {
                    int a = textLabel.Text.IndexOf("+*");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("+/"))
                {
                    int a = textLabel.Text.IndexOf("+/");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("/+"))
                {
                    int a = textLabel.Text.IndexOf("/+");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("-*"))
                {
                    int a = textLabel.Text.IndexOf("-*");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("*-"))
                {
                    int a = textLabel.Text.IndexOf("*-");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("-/"))
                {
                    int a = textLabel.Text.IndexOf("-/");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("/-"))
                {
                    int a = textLabel.Text.IndexOf("/-");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("*/"))
                {
                    int a = textLabel.Text.IndexOf("*/");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                if (textLabel.Text.Contains("/*"))
                {
                    int a = textLabel.Text.IndexOf("/*");

                    textLabel.Text = textLabel.Text.Remove(a, 1);

                }
                string str = (string)((Button)e.OriginalSource).Content;

                if (textLabel.Text == "ERROR")
                {
                    textLabel.Text = "";



                }
                if (textLabel.Text == "0")
                {
                    textLabel.Text = "";

                    textLabel.Text = str;

                }
                else if (str == "=")
                {
                    if (Check_1(textLabel.Text, "()") == 0)
                    {
                        if (Check_1(textLabel.Text, ")") == Check_1(textLabel.Text, "("))
                        {

                            string a = Convert.ToString(Calculate(textLabel.Text));

                            textLabel.Text = a;


                        }
                        else
                        {
                            if (Check_1(textLabel.Text, ")") > Check_1(textLabel.Text, "("))
                            {
                                textLabel.Text = "ERROR";
                            }
                            else
                            {
                                textLabel.Text = "ERROR";
                            }
                        }
                    }
                    else
                    {
                        textLabel.Text = "ERROR";
                    }


                }
                else if (str == "del")
                {
                    textLabel.Text = textLabel.Text.Substring(0, textLabel.Text.Length - 1);
                    if (textLabel.Text == "")
                    {
                        textLabel.Text = "0";
                    }
                }
                else if (str == "AC")
                {
                    textLabel.Text = "0";
                }
                else
                {
                    textLabel.Text += str;
                }
            }
            catch (InvalidCastException)
            {


            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();

        }

        private void Close1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void KeyEvents(object sender, KeyEventArgs e)
        {

        }
        private void TextInputEvent(object sender, TextCompositionEventArgs e)
        {

            if (textLabel.Text.Contains("="))
            {

                textLabel.Text = textLabel.Text.Remove(textLabel.Text.Length - 1);
                string value = new DataTable().Compute(textLabel.Text, null).ToString();
                textLabel.Clear();
                if (textSave.Text.Length != 0)
                {
                    textSave.Text += ";";
                    textSave.Text += value;
                }
                else
                {
                    textSave.Text = value;
                }


            }
            if (textLabel.Text == "0")
            {
                textLabel.Text = "";
            }

        }
        private void Button_GitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Ruziy");
        }
    }
}
