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
using System.Xml.Linq;

namespace Store
{
    /// <summary>
    /// Логика взаимодействия для AddZakazi.xaml
    /// </summary>
    public partial class AddZakazi : Window
    {
        OnlineStoreContext _db = new OnlineStoreContext();
        Zakazi _zakazi;
        public AddZakazi()
        {
            InitializeComponent();
            _db.Zakazis.Load();
            _db.Users.Load();
        }

        private void WinAddZakazi_Loaded(object sender, RoutedEventArgs e)
        {
            cbUserID.ItemsSource = _db.Users.ToList();
            cbUserID.DisplayMemberPath = "UserId";
                  
            if (Data.zakazi == null)
            {
                WinAddZakazi.Title = "Добавление записи";
                btnAddZakazi.Content = "Добавить";
                _zakazi = new Zakazi();
                _zakazi.DateZakaza = DateTime.Now;
            }
            else
            {
                WinAddZakazi.Title = "Изменение записи";
                btnAddZakazi.Content = "Изменить";
                _zakazi = _db.Zakazis.Find(Data.zakazi.OrderId);
            }
            WinAddZakazi.DataContext = _zakazi;
        }

        private void btnAddZakazi_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbStatus.Text.Length == 0) errors.Append("Укажите статус");
            if (tbAdress.Text.Length == 0) errors.Append("Укажите адрес");
            if (cbUserID.SelectedItem == null) errors.Append("Укажите код пользователя");
            if (dpDate.SelectedDate == null) errors.Append("Выберите дату");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (Data.zakazi == null)
                {
                    _db.Zakazis.Add(_zakazi);
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
                // Выводим полное сообщение об ошибке, включая внутреннее исключение
                MessageBox.Show($"Произошла ошибка: {ex.Message}\n\nПодробная информация:\n{ex.StackTrace}\n\nВнутренняя ошибка: {ex.InnerException?.Message ?? "Нет внутренней ошибки"}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
