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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;

namespace первый_код_на_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
                if (textSave.Text.Length!=0)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

           
            
            string  str = (string)((Button)e.OriginalSource).Content;

                if (textLabel.Text == "ERROR")
                {
                    textLabel.Text = "";



                }

                if (textLabel.Text == "0")
                {
                textLabel.Text = "";
                }
                if (textLabel.Text.Contains("--"))
                {
                    int a=textLabel.Text.IndexOf("--");
                    
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
                if (str == "AC")
                {
                    textLabel.Text = "0";
                }
                else if(str=="="){
                    if (Check_1(textLabel.Text, "()") == 0)
                    {
                        if (Check_1(textLabel.Text, ")") == Check_1(textLabel.Text, "("))
                        {
                            if (textLabel.Text.IndexOf(")")< textLabel.Text.IndexOf("("))
                            {
                                textLabel.Text = "ERROR";
                            }
                            else
                            {
                                string value = new DataTable().Compute(textLabel.Text, null).ToString();
                                textLabel.Text = value;
                            }
                            


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
            else if (str == "1/x")
            {
                double result;
                    try
                    {
                        result = Convert.ToDouble(textLabel.Text);
                        result = 1 / result;
                        textLabel.Text = result.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                 
            }
            else if (str == "del")
            {
                    try
                    {
                        textLabel.Text = textLabel.Text.Substring(0, textLabel.Text.Length - 1);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {

                        
                    }  
                
                if (textLabel.Text == "")
                {
                    textLabel.Text = "0";
                }
                    if (textLabel.Text == "ERROR")
                    {
                        textLabel.Text = "0";
                    }
                }
            else if(str=="|x|")
            {
                double dn, res;
                
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Abs(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                }
            else if (str == "cos")
            {
                double dn, res;
                
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Cos(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }

                }
            else if (str == "sin")
            {
                double dn, res;
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Sin(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                
            }
            else if (str == "n!")
            {
                double dn, res;
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Factorial(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                    
            }
            else if (str == "e")
            {
                double dn, res;

                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Exp(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                    
            }
            else if (str == "ln")
            {
                double dn, res;
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Log10(dn);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                    
            }
     
            else if (str == "x^2")
            {
                double dn, res;
                    try
                    {
                        dn = Convert.ToDouble(textLabel.Text);
                        res = Math.Pow(dn, 2);
                        textLabel.Text = res.ToString();
                    }
                    catch (System.FormatException)
                    {

                        textLabel.Text = "ERROR";
                    }
                    
            }
           
            else
            { 
            textLabel.Text += str;
            }
            double Factorial(double n)
            {
                if (n == 1) return 1;
                return n * Factorial(n - 1);
            }
            }
            catch (InvalidCastException)
            {
             
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            simplepath simplepath = new simplepath();
            simplepath.Show();
            Hide();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            simplepath simplepath = new simplepath();
            simplepath.Show();
            Hide();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Button_GitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Ruziy");
        }
    }
}
