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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ProductPrezentation.xaml
    /// </summary>
    public partial class ProductPrezentation : Window
    {
        List<Image> images = new List<Image>();
        private Utilizator utilizator;
        private Masini masina;
        SqlConnection myCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=E:\FACULTA\AN3_SEM2\INFORMATICA INDUSTRIALA\Proiect\proiect_repo\Developer-s-Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security = True");

        public ProductPrezentation(Utilizator utilizator,Masini masina)
        {
            InitializeComponent();
            this.utilizator = utilizator;
            this.masina = masina;
            emailTextBlock.Text = utilizator.email;
            var products = GetProducts();

            myCon.Open();
            DataSet dataset = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [SpecificationCar]", myCon);
            dataAdapter.Fill(dataset, "[SpecificationCar]");
            foreach (DataRow dr in dataset.Tables["[SpecificationCar]"].Rows)
            {

                int carId = Convert.ToInt32(dr.ItemArray.GetValue(1).ToString());
                if (carId ==masina.carId)
                {
                    masina.color = dr.ItemArray.GetValue(2).ToString();
                    masina.Co2E= Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                    masina.ParkingSpot= dr.ItemArray.GetValue(4).ToString();
                    masina.Consumption = Convert.ToDouble(dr.ItemArray.GetValue(5).ToString());
                    masina.Traction= dr.ItemArray.GetValue(6).ToString();
                    masina.CilindricalCap= Convert.ToDouble(dr.ItemArray.GetValue(7).ToString());
                    String[] feat= dr.ItemArray.GetValue(8).ToString().Split('@');

                    List<String> f = new List<string>();
                    for(int i = 0; i < feat.Length; i++)
                    {
                        f.Add(feat[i]);
                    }
                

                    brand.Text = masina.make;
                    model.Text = masina.model;
                    culoare.Text = masina.color;
                    combustibil.Text = masina.FuelType;
                    caiputere.Text = masina.HorsePower.ToString();
                    tractiune.Text = masina.Traction;
                    capcilindrica.Text = masina.CilindricalCap.ToString();
                    locparcare.Text = masina.ParkingSpot;
                    pret.Text = masina.carPrice.ToString();

                    features.Items.Clear();
                    foreach (String feature in f)
                    {
                        features.Items.Add(feature);
                    }

                }
               
            }
           

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
            myCon.Close();
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


