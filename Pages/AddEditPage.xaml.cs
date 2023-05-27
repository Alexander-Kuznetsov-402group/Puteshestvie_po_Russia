using Puteshestvie_po_Russia.Models;
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

namespace Puteshestvie_po_Russia.Pages
{
    public partial class AddEditPage : Page
    {
        private Tour _currentTour = new Tour();

        public AddEditPage(Tour selectedTour)
        {
            InitializeComponent();
            if (selectedTour != null)
                _currentTour = selectedTour;
            DataContext = _currentTour;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (_currentTour.TicketCount < 1 || _currentTour.TicketCount > 1000000000) error.AppendLine("Количество звезд - число от 1 до 5");

            if (string.IsNullOrWhiteSpace(_currentTour.Name)) error.AppendLine("Укажите название");

            if (_currentTour.Price <= 0) error.AppendLine("Цена не может быть меньше или равно 0");

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            if (_currentTour.Id == 0) ToursEntities.GetContext().Tour.Add(_currentTour);

            try
            {
                ToursEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TourList());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                
            }
        }
    }
}
