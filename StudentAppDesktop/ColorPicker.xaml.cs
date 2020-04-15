using System.Windows;
using System.Windows.Media;

namespace StudentAppDesktop
{

    public partial class ColorPicker : Window
    {
        public SolidColorBrush Color { get; private set; }

        public ColorPicker()
        {
            InitializeComponent();
        }

        public ColorPicker(Color oldColor)
        {
            InitializeComponent();
            ColorChoose.SelectedColor = oldColor;
        }

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            Color = new SolidColorBrush(ColorChoose.SelectedColor.Value);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
