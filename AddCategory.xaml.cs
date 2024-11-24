using Microsoft.EntityFrameworkCore;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        OnlineStoreContext _db = new();
        Category _category;
        public AddCategory()
        {
            InitializeComponent();
            _db.Categories.Load();
        }

        private void WinAddCateg_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.category == null)
            {
                WinAddCateg.Title = "Добавление записи";
                btnAddCategory.Content = "Добавить";
                _category = new Category();
            }
            else
            {
                WinAddCateg.Title = "Изменение записи";
                btnAddCategory.Content = "Изменить";
                _category = _db.Categories.Find(Data.category.CategoryId);
            }
            WinAddCateg.DataContext = _category;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbName.Text.Length == 0) errors.Append("Укажите название категории");
            if (tbOpis.Text.Length == 0) errors.Append("Напишите описание");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (Data.category == null)
                {
                    _db.Categories.Add(_category);
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
    }
}
