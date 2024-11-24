using Microsoft.Win32;
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
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Store
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }
        OnlineStoreContext _db = new OnlineStoreContext();
        Product _product;
        OpenFileDialog open = new OpenFileDialog();
        private void WinAddProduct_Loaded(object sender, RoutedEventArgs e)
        {
            cbCateg.ItemsSource = _db.Categories.ToList();
            cbCateg.DisplayMemberPath = "CategoryId";
            if(Data.product == null)
            {
                WinAddProduct.Title = "Добавление записи";
                btnAddProduct.Content = "Добавить";
                _product = new Product();
            }
            //Редактирование
            else
            {
                WinAddProduct.Title = "Изменение записи";
                btnAddProduct.Content = "Изменить";
                _product = _db.Products.Find(Data.product.ProductId);
            }
            WinAddProduct.DataContext = _product;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbName.Text.IsNullOrEmpty()) errors.Append("Укажите имя правильно");
            if (tbOpis.Text.IsNullOrEmpty()) errors.Append("Укажите описание");
            if (tbPrice.Text.IsNullOrEmpty()) errors.Append("Укажите цену");
            if (tbKolvo.Text.IsNullOrEmpty()) errors.Append("Укажите кол-во в наличие");
            if (cbCateg.SelectedItem == null) errors.Append("Выберите категорию");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //Запоминаем имя фото если оно задано
            try
            {
                if (open.SafeFileName.Length != 0)
                {
                    string newNamePhoto = Directory.GetCurrentDirectory() +
                         "\\images\\" + "_" + open.SafeFileName;
                    File.Copy(open.FileName, newNamePhoto, true);
                    _product.Image = "_" + open.SafeFileName;

                }
            }
            catch { }

            try
            {
                if (Data.product == null)
                { 
                    _db.Products.Add(_product);
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
                MessageBox.Show($"Произошла ошибка: {ex.Message}\n\nПодробная информация:\n{ex.StackTrace}\n\nВнутренняя ошибка: {ex.InnerException?.Message ?? "Нет внутренней ошибки"}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*| Файлы *.jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                BitmapImage photoImage = new BitmapImage(new Uri(open.FileName));
                ImgAdd.Source = photoImage;
            }
        }
    }
}
