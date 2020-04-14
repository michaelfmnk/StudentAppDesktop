using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System;
using MessageBox = System.Windows.MessageBox;
using StudentAppDesktop.List;

namespace StudentAppDesktop
{

    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Student> observableStudentList = new ObservableCollection<Student> { };
        public ObservableCollection<Student> ObservableStudentList
        {
            get { return observableStudentList; }
            set
            {
                observableStudentList = value;
                OnPropertyChanged("ObservableStudentList");
            }
        }
        private CustomLinkedList<Student> list = new CustomLinkedList<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChooseFontColor(object sender, RoutedEventArgs e)
        {
            var picker = new ColorPicker(((SolidColorBrush)Resources["fontColor"]).Color);
            picker.ShowDialog();
            Resources["fontColor"] = picker.Color;
        }

        private void ShowAbout(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Student Desktop App V. 1.0", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }

    public static class CustomCommands
    {
        public static RoutedUICommand ShowAbout = new RoutedUICommand("ShowAbout", 
            "ShowAbout", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.F1) });

        public static RoutedUICommand ChooseFontColor = new RoutedUICommand("ChooseFontColor",
            "ChooseFontColor", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control) });
    }
}
