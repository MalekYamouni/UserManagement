using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Model
{

    public enum UserStatus
    {
        Active,
        Inactive,
        Locked
    }
    public class UserModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private UserStatus _userStatus;
        public UserStatus Status
        {
            get => _userStatus; set
            {
                if (_userStatus != value)
                {
                    _userStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public UserModel(int id, string firstname, string lastname, string email)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
