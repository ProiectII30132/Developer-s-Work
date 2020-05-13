using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ProductPrezentation.xaml
    /// </summary>
    public partial class ProductPrezentation : Window
    {
        List<Image> images = new List<Image>();
        private Utilizator utilizator;

        public ProductPrezentation(Utilizator utilizator)
        {
            InitializeComponent();
            this.utilizator = utilizator;
            emailTextBlock.Text = utilizator.email;
            var products = GetProducts();
            if (products.Count > 0)
                PhotosList.ItemsSource = products;
            try
            {
                

                var uriSource = new Uri(@"/WpfApp1;component/"+products.ElementAt(1).Image, UriKind.Relative);
                BigImg.Source = new BitmapImage(uriSource);
                BigImg.Width = 400;
                BigImg.Height = 400;

            }
            catch(Exception ex)
            {

            }
        }

        private void ButtonAccInfo_Click(object sender, RoutedEventArgs e)
        {
            AccInfo accInfo = new AccInfo(utilizator);
            accInfo.Show();
            this.Close();
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UserMenu userMenu = new UserMenu(utilizator);
            userMenu.Show();
            this.Close();
        }

        private void PhotosList_PreviewKeyUp(object sender, KeyEventArgs e)
        {

            
        }
        private void PhotosList_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            try
            {

                var products = GetProducts();
                var uriSource = new Uri(@"/WpfApp1;component/" + products.ElementAt(PhotosList.SelectedIndex).Image, UriKind.Relative);
                BigImg.Source = new BitmapImage(uriSource);
                BigImg.Width = 400;
                BigImg.Height = 400;


            }
            catch (Exception ex)
            {

            }
        }

        private List<Images> GetProducts()
        {
            return new List<Images>()
            {
                new Images("Assests/Skoda-Kamiq1.png"),
                new Images("Assests/Skoda-Kamiq2.png"),
                new Images("Assests/Skoda-Kamiq3.png"),
                new Images("Assests/Skoda-Kamiq4.png"),
                new Images("Assests/Skoda-Kamiq1.png"),

            };
        }
    }
}


