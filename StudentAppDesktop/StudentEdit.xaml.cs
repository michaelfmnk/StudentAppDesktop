using System;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace StudentAppDesktop
{
    public partial class StudentEdit : Window
    {
        public ushort CurrentYear { get; } = (ushort) DateTime.Now.Year;
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public double AvgScore { get; private set; }
        public ushort YearOfBirth { get; private set; }

        public StudentEdit()
        {
            InitializeComponent();
            DataContext = this;
        }

        public StudentEdit(Student selectedStudent) : this()
        {
            this.FirstNameInput.Text = selectedStudent.FirstName;
            this.LastNameInput.Text = selectedStudent.LastName;
            this.MiddleNameInput.Text = selectedStudent.MiddleName;
            this.YearOfBirthInput.Value = selectedStudent.BirthYear;
            this.AvgScoreInput.Value = selectedStudent.AvgScore;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            FirstName = FirstNameInput.Text;
            MiddleName = MiddleNameInput.Text;
            LastName = LastNameInput.Text;
            AvgScore = AvgScoreInput.Value ?? 0;
            YearOfBirth = ((ushort?)YearOfBirthInput.Value) ?? CurrentYear;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateForm()
        {
           
            if (string.IsNullOrEmpty(FirstNameInput.Text))
            {
                ShowValidationDialog("First name is not provided");
                return false;
            }

            if (string.IsNullOrEmpty(MiddleNameInput.Text))
            {
                ShowValidationDialog("Middle name is not provided");
                return false;
            }

            if (string.IsNullOrEmpty(LastNameInput.Text))
            {
                ShowValidationDialog("Last name is not provided");
                return false;
            }

            if (ReferenceEquals(YearOfBirthInput.Value, null) 
                ||  YearOfBirthInput.Value < 1950 
                || YearOfBirthInput.Value > CurrentYear)
            {
                ShowValidationDialog("Invalid year of birth");
                return false;
            }

            if (ReferenceEquals(AvgScoreInput.Value, null)
                || AvgScoreInput.Value < 0
                || AvgScoreInput.Value > 100)
            {
                ShowValidationDialog("Invalid avarage score");
                return false;
            }

            return true;
        }

        private static void ShowValidationDialog(string msg)
        {
            MessageBox.Show(msg, "Please, check data!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
