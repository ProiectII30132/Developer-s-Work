using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ii-proj\Desginer-s_Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True");
        private Utilizator utilizator;
        public UserMenu(Utilizator utilizator)
        {
            InitializeComponent();
            this.utilizator = utilizator;
            emailTextBlock.Text = utilizator.email;

            myCon.Open();
            if (this.utilizator.isAdmin == 1)
            {
                List<Utilizator> utilizatori = new List<Utilizator>();
                DataSet dataset = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Admin]", myCon);
                dataAdapter.Fill(dataset, "[Admin]");
                foreach (DataRow dr in dataset.Tables["[Admin]"].Rows)
                {


                    String firstName = dr.ItemArray.GetValue(1).ToString();
                    String lastName = dr.ItemArray.GetValue(2).ToString();
                    int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                    String emailRead = dr.ItemArray.GetValue(4).ToString();
                    String passRead = dr.ItemArray.GetValue(5).ToString();

                    utilizatori.Add(new Utilizator(emailRead, passRead, admin, lastName, firstName));

                }

                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == utilizator.email)
                    {
                        utilizator.nume = utilizator1.nume;
                        utilizator.prenume = utilizator1.prenume;

                    }
                }
            }
            else
            {
                List<Utilizator> utilizatori = new List<Utilizator>();
                DataSet dataset = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Dealer]", myCon);
                dataAdapter.Fill(dataset, "[Dealer]");
                foreach (DataRow dr in dataset.Tables["[Dealer]"].Rows)
                {


                    String firstName = dr.ItemArray.GetValue(1).ToString();
                    String lastName = dr.ItemArray.GetValue(2).ToString();
                    int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                    int sales = Convert.ToInt32(dr.ItemArray.GetValue(4).ToString());
                    String emailRead = dr.ItemArray.GetValue(5).ToString();
                    String passRead = dr.ItemArray.GetValue(6).ToString();

                    utilizatori.Add(new Utilizator(emailRead, passRead, admin, lastName, firstName, sales));

                }

                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == utilizator.email)
                    {
                        utilizator.nume = utilizator1.nume;
                        utilizator.prenume = utilizator1.prenume;
                        utilizator.salesNumber = utilizator1.salesNumber;

                    }
                }

            }
            myCon.Close();

        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void PackIcon_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CarsImg_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private List<ProductCars> GetProducts()
        {
            return new List<ProductCars>()
            {
                new ProductCars("Skoda ",4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("Dacia ", 4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("BMW ", 4000, "Assests/Skoda-Kamiq.png"),
                new ProductCars("Skoda ",4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("Renault", 4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("Ford ", 4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("Ford ",4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("BMW", 4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("Dacia ", 4000, "Assests/Skoda-Kamiq.png"),
                new ProductCars("Seat ",4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("ARO ", 4000,"Assests/Skoda-Kamiq.png"),
                new ProductCars("DAC ", 4000,"Assests/Skoda-Kamiq.png"),

            };
    }

        private void CarText_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void CarStack_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void CarItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Visible;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Hidden;


            var products = GetProducts();
            if (products.Count > 0)
                ListViewProducts.ItemsSource = products;
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListViewProducts.ItemsSource);
            collectionView.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(QSTextBox.Text))
                return true;
            else
                return ((item as ProductCars).Name.IndexOf(QSTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListViewProducts.ItemsSource).Refresh();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void ParkingItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Visible;
            gridHome.Visibility = Visibility.Hidden;
        }

        private void ListViewProducts_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ProductPrezentation product = new ProductPrezentation(utilizator);
            product.Show();
            this.Close();
        }

        private void ButtonAccInfo_Click(object sender, RoutedEventArgs e)
        {
            AccInfo accInfo = new AccInfo(utilizator);
            accInfo.Show();
            this.Close();

        }

        private void ButtonHome_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Visible;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
