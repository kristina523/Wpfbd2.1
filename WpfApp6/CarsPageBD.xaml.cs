using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfApp6
{
    public partial class CarsPageBD : Page
    {
        private CarsShowroommEntities cars = new CarsShowroommEntities();
        public CarsPageBD()
        {
            InitializeComponent();
            BD_Cars.ItemsSource = cars.Car.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;

            Car v = new Car();
            v.Brand_ID = Convert.ToInt32(text1.Text);
            v.Years = text3.Text;

            decimal price;
            try
            {
                if (Decimal.TryParse(text4.Text, NumberStyles.AllowDecimalPoint, culture, out price))
                {
                    v.Price = price;

                    cars.Car.Add(v);
                    cars.SaveChanges();
                    BD_Cars.ItemsSource = cars.Car.ToList();
                }
                else
                {
                    MessageBox.Show("Неверный формат цены. Пожалуйста, введите корректное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Не существует машина с таким брендом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (BD_Cars.SelectedItem != null)
            {
                var selected = BD_Cars.SelectedItem as Car;

                if (selected != null)
                {
                    selected.Brand_ID = Convert.ToInt32(text1.Text);
                    selected.Years = text3.Text;
                    selected.Price = Convert.ToDecimal(text4.Text);

                    cars.SaveChanges();
                    BD_Cars.ItemsSource = cars.Car.ToList();
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedVehicles = BD_Cars.SelectedItem as Car;
            if (selectedVehicles != null)
            {
                cars.Car.Remove(selectedVehicles);
                cars.SaveChanges();
                BD_Cars.ItemsSource = cars.Car.ToList();
            }
        }

        private void BD_Vehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (BD_Cars.SelectedItem != null)
                {
                    var selected = BD_Cars.SelectedItem as Car;

                    if (selected != null)
                    {
                        text1.Text = selected.Brand_ID.ToString();
                        text3.Text = selected.Years;
                        text4.Text = selected.Price.ToString();

                        cars.SaveChanges();
                        BD_Cars.ItemsSource = cars.Car.ToList();
                    }
                }
            }
            catch { }
        }
    }
}