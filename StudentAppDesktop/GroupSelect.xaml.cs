using StudentAppDesktop.List;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StudentAppDesktop
{
    public partial class GroupSelect : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<NamedCustomLinkedList<NamedCustomLinkedList<Student>>> uni;
        
        private NamedCustomLinkedList<NamedCustomLinkedList<Student>> selectedFaculty;
        private NamedCustomLinkedList<Student> selectedGroup;

        private ObservableCollection<NamedCustomLinkedList<NamedCustomLinkedList<Student>>> observableFacultyList = new ObservableCollection<NamedCustomLinkedList<NamedCustomLinkedList<Student>>> { };
        private ObservableCollection<NamedCustomLinkedList<Student>> observableGroupList = new ObservableCollection<NamedCustomLinkedList<Student>> { };

        public NamedCustomLinkedList<Student> SelectedGroup
        {
            get {
                return selectedGroup;
            }
            set
            {
                selectedGroup = value;
                UpdateCanDeleteStatuses();
                OnPropertyChanged("SelectedGroup");
            }
        }

        public NamedCustomLinkedList<NamedCustomLinkedList<Student>> SelectedFaculty
        {
            get
            {
                return selectedFaculty;
            }
            set
            {
                selectedFaculty = value;
                RefreshGroups();
                UpdateCanDeleteStatuses();
                OnPropertyChanged("SelectedFaculty");
            }
        }
        public ObservableCollection<NamedCustomLinkedList<NamedCustomLinkedList<Student>>> ObservableFacultyList
        {
            get
            {
                return observableFacultyList;
            }
            set
            {
                observableFacultyList = value;
                OnPropertyChanged("ObservableFacultyList");
            }
        }
        public ObservableCollection<NamedCustomLinkedList<Student>> ObservableGroupList 
        {
            get
            {
                return observableGroupList;
            }
            set
            {
                observableGroupList = value;
                OnPropertyChanged("ObservableGroupList");
            }
        }

        public void UpdateCanDeleteStatuses()
        {
            Resources["CanDeleteFaculty"] = uni
            .Where(it => !ReferenceEquals(selectedFaculty, it))
            .Select(it => it.Size)
            .Sum() > 0;

            Resources["CanDeleteGroup"] = (selectedFaculty?.Size ?? 0) > 0 && uni.Select(it => it.Size).Sum() > 1;
        }

        public GroupSelect(List<NamedCustomLinkedList<NamedCustomLinkedList<Student>>> uni)
        {
            InitializeComponent();
            this.uni = uni;
            DataContext = this;
            Refresh();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Refresh()
        {
            observableFacultyList.Clear();
            observableGroupList.Clear();
            selectedFaculty = null;
            selectedGroup = null;

            uni.Sort();

            foreach (var faculty in uni)
            {
                observableFacultyList.Add(faculty);
            }

            if (observableFacultyList.Count != 0)
            {
                selectedFaculty = uni[0];

                RefreshGroups();
            }
            UpdateCanDeleteStatuses();
        }

        private void RefreshGroups()
        {
            observableGroupList.Clear();
            if (!(selectedFaculty is null))
            {
                selectedFaculty.Sort();

                foreach (var group in selectedFaculty)
                {
                    observableGroupList.Add(group);
                }

                if (observableGroupList.Count != 0)
                {
                    selectedGroup = selectedFaculty.Get(0);
                }
            }

        }

        private void AddFaculty(object sender, RoutedEventArgs e)
        {
            var nameForm = CreateNameForm("faculty");

            if (nameForm.ShowDialog() ?? false)
            {
                uni.Add(new NamedCustomLinkedList<NamedCustomLinkedList<Student>>(nameForm.Value));
                Refresh();
            }
        }

        private void AddGroup(object sender, RoutedEventArgs e)
        {
            var nameForm = CreateNameForm("group");
            if (nameForm.ShowDialog() ?? false)
            {
                selectedFaculty.PushToEnd(new NamedCustomLinkedList<Student>(nameForm.Value));
                RefreshGroups();
            }
        }

        private SimpleNameForm CreateNameForm(string entityName)
        {
            var nameForm = new SimpleNameForm(entityName);

            nameForm.Resources["fontColor"] = Resources["fontColor"];
            nameForm.Resources["fontFamily"] = Resources["fontFamily"];
            nameForm.Resources["fontBold"] = Resources["fontBold"];
            nameForm.Resources["fontSize"] = Resources["fontSize"];
            return nameForm;
        }

        private void DeleteCurrentFaculty(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                uni.Remove(selectedFaculty);

                Refresh();
            }
        }

        private void DeleteCurrentGroup(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes && !(selectedFaculty is null))
            {
                selectedFaculty.DeleteFirst(selectedGroup);
                Refresh();
                OnPropertyChanged("CanDeleteGroup");
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
