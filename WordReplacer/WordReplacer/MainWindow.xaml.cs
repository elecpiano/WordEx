using System;
using System.Text.RegularExpressions;
using System.Windows;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using Microsoft.Win32;

namespace WordReplacer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Process(string param1, string param2)
        {
            sourceString = Regex.Replace(sourceString, param1, param2);
        }

        //string expression_1 = @"([tm])a(ke)";
        //string expression_2 = @"$1x$2";

        string sourceString;
        private void Source_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                sourceString = File.ReadAllText(dialog.FileName);
            }

            resultTextBlock.Text = sourceString;
        }

        CsvReader csv;
        private void Rule_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                csv = new CsvReader(new StreamReader(dialog.FileName), true);
            }
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            while (csv.ReadNextRecord())
            {
                Process(csv[0], csv[1]);
            }

            resultTextBlock.Text = sourceString;
        }

    }
}
