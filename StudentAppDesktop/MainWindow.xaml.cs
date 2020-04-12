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

namespace StudentAppDesktop
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


        public void OnAboutClicked(object sender, RoutedEventArgs e)
        {
            ShowHelp();
        }
        public void ShowDialogCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ShowHelp();
        }

        private static void ShowHelp()
        {
            MessageBox.Show("Student App V_1.0");
        }

    }
    
}
