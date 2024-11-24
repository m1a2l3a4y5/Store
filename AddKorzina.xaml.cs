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
    /// Логика взаимодействия для AddKorzina.xaml
    /// </summary>
    public partial class AddKorzina : Window
    {
        OnlineStoreContext _db = new();
        Korzina _korzina;
        public AddKorzina()
        {
            InitializeComponent();
            _db.Korzinas.Load();
            _db.Users.Load();
            _db.Products.Load();
        }

        private void WinAddKorzina_Loaded(object sender, RoutedEventArgs e)
        {
            cbIdItem.ItemsSource = _db.Products.ToList();
            cbIdItem.DisplayMemberPath = "ProductId";
            cbIdUser.ItemsSource = _db.Users.ToList();
            cbIdUser.DisplayMemberPath = "UserId";
            if (Data.korzina == null)
            {
                WinAddKorzina.Title = "Добавление записи";
                AddRecord.Content = "Добавить";
                _korzina = new Korzina();
            }
            else
            {
                WinAddKorzina.Title = "Изменение записи";
                AddRecord.Content = "Изменить";
                _korzina = _db.Korzinas.Find(Data.korzina.CartId);
            }
            WinAddKorzina.DataContext = _korzina;
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (cbIdItem.SelectedItem == null) errors.Append("Выберите товар");
            if (cbIdUser.SelectedItem == null) errors.Append("Выберите пользователя");
            if (tbKolItems.Text.Length == 0) errors.Append("Укажите количество товара");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.korzina == null)
                {
                    _db.Korzinas.Add(_korzina);
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

        private void ExitWin_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
