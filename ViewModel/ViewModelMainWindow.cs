using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserManagement.Model;
using UserManagement.Relay;
using UserManagement.ViewModel;

namespace UserManagement.ViewModel
{
    public class ViewModelMainWindow : ViewBase
    {

        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;
        private int _userId;
        private string _newFirstName;
        private string _newLastName;
        private string _newEmail;

        public RelayCommand AddNewUserCommand => new RelayCommand(execute => AddNewUser(), canExecute => !string.IsNullOrWhiteSpace(NewFirstName) && !string.IsNullOrWhiteSpace(NewLastName) && !string.IsNullOrWhiteSpace(NewEmail));
        public RelayCommand MarkAsInactiveCommand => new RelayCommand(execute => MarkAsInactive(), canExecute => SelectedUser != null && SelectedUser.Status != UserStatus.Inactive);
        public RelayCommand MarkAsActiveCommand => new RelayCommand(execute => MarkAsActive(), canExecute => SelectedUser != null && SelectedUser.Status != UserStatus.Active);
        public RelayCommand MarkAsLockedCommand => new RelayCommand(execute => MarkAsLocked(), canExecute => SelectedUser != null && SelectedUser.Status != UserStatus.Locked);


        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewModelMainWindow()
        {
            _users = new ObservableCollection<UserModel> {
                    new UserModel(1, "John", "Doe", "john_doe@gmx.de"),
                    new UserModel(2, "Malek", "Yamouni", "malek_yamouni@gmx.de"),
                    new UserModel(3, "Jean", "Zimmermann", "jean_Zimmermann@gmx.de")
            };
        }

        public ObservableCollection<UserModel> UserCollection
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewFirstName
        {
            get { return _newFirstName; }
            set
            {
                if (_newFirstName != value)
                {
                    _newFirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewLastName
        {
            get { return _newLastName; }
            set
            {
                if (_newLastName != value)
                {
                    _newLastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewEmail
        {
            get { return _newEmail; }
            set
            {
                if (_newEmail != value)
                {
                    _newEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        public int GetMaxUserId()
        {
            if (_users == null || _users.Count == 0)
            {
                return 0;
            }

            return _users.Max(u => u.Id);
        }

        public void AddNewUser()
        {
            if (string.IsNullOrWhiteSpace(NewFirstName) || string.IsNullOrWhiteSpace(NewLastName) || string.IsNullOrWhiteSpace(NewEmail))
            {
                MessageBox.Show("Please fill in all fields to add a new user.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int newId = GetMaxUserId() + 1;
            var user = new UserModel(newId, NewFirstName, NewLastName, NewEmail);
            NewUserMarkAsActive(user);
            UserCollection.Add(user);

            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            NewEmail = string.Empty;
        }

        public UserStatus NewUserMarkAsActive(UserModel user)
        {
            if (user != null)
            {
                user.Status = UserStatus.Active;
            }
            return user.Status;
        }

        public UserStatus MarkAsActive()
        {
            if (_selectedUser != null)
            {
                _selectedUser.Status = UserStatus.Active;
                return _selectedUser.Status;
            }
            else
            {
                MessageBox.Show("Please select a User", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return UserStatus.Active;
            }
        }

        public UserStatus MarkAsInactive()
        {
            if (_selectedUser != null)
            {
                _selectedUser.Status = UserStatus.Inactive;
                return _selectedUser.Status;
            }
            else
            {
                MessageBox.Show("Please select a User", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return UserStatus.Active;
            }
        }


        public UserStatus MarkAsLocked()
        {
            if (_selectedUser != null)
            {
                _selectedUser.Status = UserStatus.Locked;
                return _selectedUser.Status;
            }
            else
            {
                MessageBox.Show("Please select a User", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return UserStatus.Active;
            }
        }
    }
}
