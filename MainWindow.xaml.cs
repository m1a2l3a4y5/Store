using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Store.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Store
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
            LoadDBInListView();
        }
        void LoadDBInListView()
        {
            using (OnlineStoreContext _db = new())
            {
                _db.Products.Load();
                int selectedIndex = listView.SelectedIndex;
                listView.ItemsSource = _db.Products.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == listView.Items.Count) selectedIndex--;
                    listView.SelectedIndex = selectedIndex;
                    listView.ScrollIntoView(listView.SelectedItem);
                    
                }
                listView.Focus();
            }

        }
        void LoadDBInDataGrid()
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                int selectedIndex = dGCategory.SelectedIndex;
                selectedIndex = dGKorzina.SelectedIndex;
                selectedIndex = dGUser.SelectedIndex;
                selectedIndex = dGZakazi.SelectedIndex;

                _db.Categories.Load();
                _db.Korzinas.Load();
                _db.Users.Load();
                _db.Zakazis.Load();

                dGCategory.ItemsSource = _db.Categories.ToList();
                dGKorzina.ItemsSource = _db.Korzinas.ToList();
                dGUser.ItemsSource = _db.Users.ToList();
                dGZakazi.ItemsSource = _db.Zakazis.ToList();

                if(selectedIndex != -1)
                {
                    if (selectedIndex == dGCategory.Items.Count && selectedIndex == dGKorzina.Items.Count &&
                        selectedIndex == dGUser.Items.Count && selectedIndex == dGZakazi.Items.Count) selectedIndex--;
                    dGCategory.SelectedIndex = selectedIndex;
                    dGKorzina.SelectedIndex = selectedIndex;
                    dGUser.SelectedIndex = selectedIndex;
                    dGZakazi.SelectedIndex = selectedIndex;
                    dGCategory.ScrollIntoView(dGCategory.SelectedItem);
                    dGKorzina.ScrollIntoView(dGCategory.SelectedItem);
                    dGUser.ScrollIntoView(dGCategory.SelectedItem);
                    dGZakazi.ScrollIntoView(dGCategory.SelectedItem);
                }
                dGCategory.Focus();
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Data.user = null;
            AddUser f = new AddUser();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dGUser.SelectedItem != null)
            {
                Data.user = (User)dGUser.SelectedItem;
                AddUser f = new AddUser();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }
            else MessageBox.Show("Выберите элемент для редактирования","Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            Data.category = null;
            AddCategory f = new AddCategory();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void btnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dGCategory.SelectedItem != null)
            {
                Data.category = (Category)dGCategory.SelectedItem;
                AddCategory f = new AddCategory();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }
            else MessageBox.Show("Выберите элемент для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Data.product = null;
            AddProduct f = new AddProduct();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInListView();
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                Data.product = (Product)listView.SelectedItem;
                AddProduct f = new AddProduct();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInListView();
            }
            else MessageBox.Show("Выберите элемент для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddZakazi_Click(object sender, RoutedEventArgs e)
        {
            Data.zakazi = null;
            AddZakazi f = new AddZakazi();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void btnEditZakazi_Click(object sender, RoutedEventArgs e)
        {
            if (dGZakazi.SelectedItem != null)
            {
                Data.zakazi = (Zakazi)dGZakazi.SelectedItem;
                AddZakazi f = new AddZakazi();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }
            else MessageBox.Show("Выберите элемент для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddKorzina_Click(object sender, RoutedEventArgs e)
        {
            Data.korzina = null;
            AddKorzina f = new AddKorzina();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }

        private void btnEditKorzina_Click(object sender, RoutedEventArgs e)
        {
            if (dGKorzina.SelectedItem != null)
            {
                Data.korzina = (Korzina)dGKorzina.SelectedItem;
                AddKorzina f = new AddKorzina();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }
            else MessageBox.Show("Выберите элемент для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dGCategory.SelectedItem != null)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        //получаем текущую запись
                        Category row = (Category)dGCategory.SelectedItem;
                        if (row != null)
                        {
                            using (OnlineStoreContext db = new())
                            {
                                //удаляем запись
                                db.Categories.Remove(row);
                                db.SaveChanges();
                            }
                            LoadDBInDataGrid();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления!", "Ошибка");
                    }
                }
                else
                    dGCategory.Focus();
            }
            else MessageBox.Show("Выберите элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        { 
            if (listView.SelectedItem != null)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        //получаем текущую запись
                        Product row = (Product)listView.SelectedItem;
                        if (row != null)
                        {
                            using (OnlineStoreContext db = new())
                            {
                                //удаляем запись
                                db.Products.Remove(row);
                                db.SaveChanges();
                            }
                            LoadDBInListView();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления!", "Ошибка");
                    }
                }
                else
                    listView.Focus();
            }
            else MessageBox.Show("Выберите элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteZakazi_Click(object sender, RoutedEventArgs e)
        {
            if (dGZakazi.SelectedItem != null)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        //получаем текущую запись
                        Zakazi row = (Zakazi)dGZakazi.SelectedItem;
                        if (row != null)
                        {
                            using (OnlineStoreContext db = new())
                            {
                                //удаляем запись
                                db.Zakazis.Remove(row);
                                db.SaveChanges();
                            }
                            LoadDBInDataGrid();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления!", "Ошибка");
                    }
                }
                else
                    dGZakazi.Focus();
            }
            else MessageBox.Show("Выберите элемент удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteKorzina_Click(object sender, RoutedEventArgs e)
        {
            if (dGKorzina.SelectedItem != null)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        //получаем текущую запись
                        Korzina row = (Korzina)dGKorzina.SelectedItem;
                        if (row != null)
                        {
                            using (OnlineStoreContext db = new())
                            {
                                //удаляем запись
                                db.Korzinas.Remove(row);
                                db.SaveChanges();
                            }
                            LoadDBInDataGrid();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления!", "Ошибка");
                    }
                }
                else
                    dGKorzina.Focus();
            }
            else MessageBox.Show("Выберите элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dGUser.SelectedItem != null)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        //получаем текущую запись
                        User row = (User)dGUser.SelectedItem;
                        if (row != null)
                        {
                            using (OnlineStoreContext db = new())
                            {
                                //удаляем запись
                                db.Users.Remove(row);
                                db.SaveChanges();
                            }
                            LoadDBInDataGrid();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления!", "Ошибка");
                    }
                }
                else
                    dGUser.Focus();
            }
            else MessageBox.Show("Выберите элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ResetDataBase_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
            LoadDBInListView();
        }
        private bool isDarkMode = false; // Флаг для отслеживания текущей темы
        private void ResetBackground_Click(object sender, RoutedEventArgs e)
        {
            // Определяем текущие цвета
            string lightThemeHex = "#FFFFFF";
            string darkThemeHex = "#434445";

            // Получаем текущий цвет фона первого элемента
            var currentBackground = grid1.Background as SolidColorBrush;

            // Проверяем, какая сейчас тема
            if (isDarkMode)
            {
                // Сейчас темно, переключаемся на светлый режим
                SetBackground(lightThemeHex);
                isDarkMode = false;
            }
            else
            {
                // Сейчас светло, переключаемся на темный режим
                SetBackground(darkThemeHex);
                isDarkMode = true;
            }
        }

        private void SetBackground(string hexColor)
        {
            // Устанавливаем новый цвет фона для всех элементов
            grid1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            grid2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            grid3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            grid4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            grid5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tab1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tab2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tab3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tab4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tab5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            menu1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            mainWin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tbTotalKol.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            tbTotalSum.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
        }
        
        private void btnTotalKol_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                int totalQuantity = _db.Products.Sum(product => product.KolvoVnalichie);
                tbTotalKol.Text = $"{totalQuantity}";
            }
        }

        private void btnTotalSum_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                double totalSum = _db.Products.Sum(product => product.Price * product.KolvoVnalichie);
                tbTotalSum.Text = $"{totalSum}";
            }
           
        }

        private void btnClearProduct_Click(object sender, RoutedEventArgs e)
        {
            tbTotalSum.Clear();
            tbTotalKol.Clear();
        }

        private void btnNameA_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                var nameA = _db.Users.Where(p => p.Name.StartsWith("А"));
                dGUser.ItemsSource = nameA.ToList();
            }
        }

        private void btnSortCat_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                var sort = _db.Categories.OrderBy(c => c.Name);
                dGCategory.ItemsSource = sort.ToList();
            }
        }

        private void btnStatusDone_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                var done = _db.Zakazis.Where(p => p.Status.Contains("Доставлен"));
                dGZakazi.ItemsSource = done.ToList();
            }
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                var admins = _db.Users.Where(p => p.Role.Contains("Админи"));
                dGUser.ItemsSource = admins.ToList();
            }
        }

        private void btnFirstTwoMonth_Click(object sender, RoutedEventArgs e)
        {
            using (OnlineStoreContext _db = new OnlineStoreContext())
            {
                DateTime startDate = new DateTime(2023, 1, 1); // Начало января текущего года
                DateTime endDate = new DateTime(2023, 2, 28);  // Конец февраля текущего года

                var ordersInFirstTwoMonths = _db.Zakazis
                                               .Where(o => o.DateZakaza >= startDate && o.DateZakaza <= endDate).ToList();


                dGZakazi.ItemsSource = ordersInFirstTwoMonths.ToList(); ;
            }
        }
    }
}