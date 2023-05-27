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
    public partial class TourList : Page
    {

        public TourList()
        {
            InitializeComponent();
            DTours.ItemsSource = ToursEntities.GetContext().Tour.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Redactor_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Tour));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DTours.ItemsSource = ToursEntities.GetContext().Tour.ToList();
            }
        }
    }
}
