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
        bool isEdit = false;
        public UserFormPage(User? _editStudent = null)
        {
            InitializeComponent();
            if (_editStudent != null)
            {
                _user = _editStudent;
                isEdit = true;
            }
            DataContext = _user;
        }
        private void save(object sender, RoutedEventArgs e)
        {
            if (isEdit)
                _service.Commit();
            else
                _service.Add(_user);
            NavigationService.GoBack();
        }
        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
