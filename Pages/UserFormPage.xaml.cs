using pract12_trpo.Data;
using pract12_trpo.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pract12_trpo.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentFormPage.xaml
    /// </summary>
    public partial class UserFormPage :Page
    {
        private UsersService _service = new();
        public User _user = new();

        // Свойство для привязки в XAML
        public bool IsEdit { get; private set; } = false;

        public UserFormPage(User? editUser = null)
        {
            InitializeComponent();

            if (editUser != null)
            {
                _user = editUser;
                IsEdit = true;
            }

            DataContext = _user;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            // Принудительно обновляем источники привязки
            LoginUser.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            NameUser.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            EmailUser.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            PasswordUser.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            // Проверяем валидацию
            if (!IsFormValid())
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в форме",
                    "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Устанавливаем дату создания ТОЛЬКО для новых пользователей
            if (!IsEdit)
            {
                _user.CreateAt = DateTime.Now;
            }

            try
            {
                if (IsEdit)
                    _service.Commit();
                else
                    _service.Add(_user);

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private bool IsFormValid()
        {
            // Проверяем все поля на наличие ошибок валидации
            var textBoxes = new[] { LoginUser, NameUser, EmailUser, PasswordUser };

            foreach (var textBox in textBoxes)
            {
                if (Validation.GetHasError(textBox))
                    return false;
            }

            return true;
        }
    }
}
