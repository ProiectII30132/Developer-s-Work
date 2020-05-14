using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
        private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ii-proj\Developer-s-Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True");
        private Utilizator utilizator;
        private List<Utilizator> dealers= new List<Utilizator>();
        public UserMenu(Utilizator utilizator)
        {
            InitializeComponent();
            this.utilizator = utilizator;
            emailTextBlock.Text = utilizator.email;
            myCon.Open();
            if (this.utilizator.isAdmin == 1)
            {
                infoDealearItem.Visibility = Visibility.Visible;
                StatisticItem.Visibility = Visibility.Visible;
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

        public List<Utilizator> ReadDealers()
        {
            List<Utilizator> dealers = new List<Utilizator>();
            myCon.Open();
            DataSet dataset = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [user]", myCon);
            dataAdapter.Fill(dataset, "[user]");
            foreach (DataRow dr in dataset.Tables["[user]"].Rows)
            {
                int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                String emailRead = dr.ItemArray.GetValue(1).ToString();
                String passRead = dr.ItemArray.GetValue(2).ToString();
                double salary = Convert.ToDouble(dr.ItemArray.GetValue(4).ToString());
                if(admin ==0)
                    dealers.Add(new Utilizator(emailRead, passRead, admin, salary));
            }

            List<Utilizator> utilizatori = new List<Utilizator>();
            dataAdapter = new SqlDataAdapter("SELECT * FROM [Dealer]", myCon);
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
            foreach (Utilizator dealar in dealers)
            {
                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == dealar.email)
                    {
                        dealar.nume = utilizator1.nume;
                        dealar.prenume = utilizator1.prenume;
                        dealar.salesNumber = utilizator1.salesNumber;

                    }
                }
            }
            myCon.Close();
            return dealers;

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
            gridDealerInfo.Visibility = Visibility.Hidden;

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
            gridDealerInfo.Visibility = Visibility.Hidden;



        }

        private void infoDealearItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Hidden;
            gridDealerInfo.Visibility = Visibility.Visible;
            dealers.Clear();
            dealrLB.Items.Clear();
            dealers  = ReadDealers();
            foreach (Utilizator dealer in dealers)
            {
                dealrLB.Items.Add(dealer.nume +" "+ dealer.prenume);
            }
        }
        private void dealrLB_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SalaryTB.Text = "" + dealers.ElementAt(dealrLB.SelectedIndex).salary;
                SalesNumberTB.Text = "" + dealers.ElementAt(dealrLB.SelectedIndex).salesNumber;
            }
            catch (Exception EX)
            {
                MessageBox.Show("bug");
            }
        }

        private void dealerFireBt_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            myCon.Open();
            Utilizator delDealer = dealers.ElementAt(dealrLB.SelectedIndex);
            dealrLB.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            
            try
            {
                
                cmd = new SqlCommand("DELETE FROM [Dealer] WHERE Email= @email", myCon);
                cmd.Parameters.AddWithValue("email", delDealer.email);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM [user] WHERE Email= @email", myCon);
                cmd.Parameters.AddWithValue("email", delDealer.email);
                cmd.ExecuteNonQuery();
                ok = true;

            }
            catch (Exception ex)
            {
                new MessageBoxPoni("Error").Show();
                ok = false;
            }
            myCon.Close();

            dealers.Clear();
            dealers = ReadDealers();
            foreach(Utilizator utilizator in dealers)
            {
                dealrLB.Items.Add(utilizator.nume + " " + utilizator.prenume);
            }
            if (ok)
            {
                new MessageBoxPoni("Dealer Fired").Show();

            }
        }

        private void addDealerBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilizator test = null;
                SignUp signUp = new SignUp(test);
                signUp.ShowDialog();
                int i=0;
                
                test = signUp.getUtil();
                signUp.Close();
                dealers.Add(test);
                dealrLB.Items.Add(test.nume+' '+test.prenume);
             
            }
            catch(Exception ex)
            {
                new MessageBoxPoni("Dealer Aded").Show();
            }
        }

        private void promoteDealerBt_Click(object sender, RoutedEventArgs e)
        {
            gridChangeS.Visibility = Visibility.Visible;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand();
            bool ok = false;
            try
           {
                Utilizator upDealer = dealers.ElementAt(dealrLB.SelectedIndex);
                myCon.Open();
                cmd = new SqlCommand("UPDATE [user] SET Salary=@Salary WHERE [Email]=@Email", myCon);
                cmd.Parameters.AddWithValue("@Email",upDealer.email);
                cmd.Parameters.AddWithValue("@Salary", SumTB.Text);
                cmd.ExecuteNonQuery();
                ok = true;
            }
            catch (Exception ex)
            {
                new MessageBoxPoni("Dealer not selected").Show();
                ok = false;
            }
            myCon.Close();
            SumTB.Text ="";
            gridChangeS.Visibility = Visibility.Hidden;
            dealers.Clear();
            dealers = ReadDealers();
            dealrLB.Items.Clear();
            foreach (Utilizator utilizator in dealers)
            {
                dealrLB.Items.Add(utilizator.nume + " " + utilizator.prenume);
            }
            if (ok)
            {
                new MessageBoxPoni("Salary Changed").Show();

            }
        }
    

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
