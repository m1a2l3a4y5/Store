using Microsoft.EntityFrameworkCore;
using Store.Model;
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
using System.Windows.Shapes;

namespace Store
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        OnlineStoreContext _db = new();
        User _user;
        public AddUser()
        {
            InitializeComponent();
            _db.Users.Load();
        }

        private void WinAddUser_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.user == null)
            {
                WinAddUser.Title = "Добавление записи";
                btnAddUser.Content = "Добавить";
                _user = new User();
                _user.RegistrationDate = DateTime.Now;
            }
            else
            {
                WinAddUser.Title = "Изменение записи";
                btnAddUser.Content = "Изменить";
                _user = _db.Users.Find(Data.user.UserId);
            }
            WinAddUser.DataContext = _user;
        }
    

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbname.Text.Length == 0) errors.Append("Укажите имя");
            if (tbemail.Text.Length == 0) errors.Append("Укажите почту");
            if (tbpassword.Text.Length == 0) errors.Append("Укажите пароль");
            if (tbrole.Text.Length == 0) errors.Append("Укажите роль");
            if (dpDate.SelectedDate == null) errors.Append("Выберите дату");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.user == null)
                {
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
