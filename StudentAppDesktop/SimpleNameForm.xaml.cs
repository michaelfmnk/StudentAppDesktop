using System.Windows;

namespace StudentAppDesktop
{
    public partial class SimpleNameForm : Window
    {
        public string EntityName { get; private set; }
        public string Value { get; private set; }

        public SimpleNameForm(string enityName)
        {
            InitializeComponent();
            DataContext = this;
            EntityName = enityName;
        }

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EnityNameInput.Text))
            {
                MessageBox.Show("Input is empty", "Please, check data!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Value = EnityNameInput.Text;
            Close();
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
