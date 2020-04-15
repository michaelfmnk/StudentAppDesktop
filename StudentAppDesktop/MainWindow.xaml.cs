using StudentAppDesktop.List;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace StudentAppDesktop
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Student selectedStudent;

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

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

        private readonly CustomLinkedList<Student> list = new CustomLinkedList<Student>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Refresh();
        }

        public void Refresh()
        {
            observableStudentList.Clear();
            list.Sort();

            foreach (var student in list)
            {
                observableStudentList.Add(student);
            }

            if (observableStudentList.Count != 0)
            {
                SelectedStudent = list.Get(0);
                Resources["UserInfoVisibility"] = Visibility.Visible;
                Resources["BannerVisibility"] = Visibility.Hidden;
                Resources["EditButtonsEnabled"] = true;
            }
            else
            {
                Resources["UserInfoVisibility"] = Visibility.Hidden;
                Resources["BannerVisibility"] = Visibility.Visible;
                Resources["EditButtonsEnabled"] = false;
            }
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
            if (picker.ShowDialog() ?? false)
            {
                Resources["fontColor"] = picker.Color;

            }
        }

        private void ChooseFont(object sender, RoutedEventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.MaxSize = 14;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Resources["fontSize"] = (double) fontDialog.Font.Size;

                var tempFamily = fontDialog.Font.Name;

                if (fontDialog.Font.Italic)
                {
                    tempFamily += " Italic";
                }

                Resources["fontFamily"] = new FontFamily(tempFamily);
                Resources["fontBold"] = fontDialog.Font.Bold ? FontWeight.FromOpenTypeWeight(800) : FontWeight.FromOpenTypeWeight(400);
            }
        }

        private void ShowAbout(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Student Desktop App V. 1.0", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            var sf = new StudentEdit();
            sf.Resources["fontColor"] = Resources["fontColor"];
            sf.Resources["fontFamily"] = Resources["fontFamily"];
            sf.Resources["fontBold"] = Resources["fontBold"];
            sf.Resources["fontSize"] = Resources["fontSize"];
            if (sf.ShowDialog() ?? false)
            {
                list.PushToEnd(new Student(sf.FirstName, sf.LastName, sf.MiddleName, sf.YearOfBirth, sf.AvgScore));
                Refresh();
            }
        }

        private void DeleteAllStudents(object sender, RoutedEventArgs e)
        {
            list.Clear();
            Refresh();
        }

        private void DeleteSelectedStudent(object sender, RoutedEventArgs e)
        {
            if (selectedStudent is object)
            {
                list.DeleteFirst(selectedStudent);
                Refresh();
            }
        }

        private void UpdateSelectedStudent(object sender, RoutedEventArgs e)
        {
            var sf = new StudentEdit(selectedStudent);
            
            if (sf.ShowDialog() ?? false)
            {
                selectedStudent.FirstName = sf.FirstName;
                selectedStudent.LastName = sf.LastName;
                selectedStudent.MiddleName = sf.MiddleName;
                selectedStudent.AvgScore = sf.AvgScore;
                selectedStudent.BirthYear = sf.YearOfBirth;
                Refresh();
            }
        }

        private void HightlightStudent(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var inputText = ((System.Windows.Controls.TextBox)sender).Text.ToLower();
            for (var i = 0; i < observableStudentList.Count; i++)
            {
                
                if (observableStudentList[i].FullName.ToLower().Contains(inputText))
                {
                    SelectedStudent = observableStudentList[i];
                    return;
                }
            }
        }
    }

    public static class CustomCommands
    {
        public static RoutedUICommand ShowAbout = new RoutedUICommand("ShowAbout", 
            "ShowAbout", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.F1) });

        public static RoutedUICommand ChooseFontColor = new RoutedUICommand("ChooseFontColor",
            "ChooseFontColor", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control) });

        public static RoutedUICommand ChooseFont = new RoutedUICommand("ChooseFont",
            "ChooseFont", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.F, ModifierKeys.Control) });

        public static RoutedUICommand AddStudent = new RoutedUICommand("AddStudent",
            "AddStudent", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.Enter, ModifierKeys.Control) });

        public static RoutedUICommand EditStudent = new RoutedUICommand("EditStudent",
            "EditStudent", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.F2) });

        public static RoutedUICommand Close = new RoutedUICommand("Close",
            "Close", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.Escape) });

        public static RoutedUICommand DeleteSelectedStudent = new RoutedUICommand("DeleteSelectedStudent",
            "DeleteSelectedStudent", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.Delete) });

        public static RoutedUICommand DeleteAllStudents = new RoutedUICommand("DeleteAllStudents",
            "DeleteAllStudents", typeof(MainWindow), new InputGestureCollection() { new KeyGesture(Key.Delete, ModifierKeys.Shift) });
    }
}
