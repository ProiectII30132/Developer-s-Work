using System;
using System.Collections.Generic;
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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private SqlCommand cmd;
        private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\FACULTA\AN3_SEM2\INFORMATICA INDUSTRIALA\Proiect\Proiect_varianta_finala\Developer-s-Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True");


        Utilizator utilizator;
        public SignUp(Utilizator utilizator)
        {
            InitializeComponent();
            this.utilizator = utilizator;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (emailTB.Text == "" || PassP.Password == "" || SallaryTB.Text == "" || FirstNameTB.Text == "" || LastNameTB.Text == "")
            {
                new MessageBoxPoni("All Blocks must be completed").Show();
                return;
            }
            try
            {
             
                int emailok = 1, firstletterfn = 1, firstletterln = 1, lnok = 1, fnok = 1,salaryok=1; 

                string[] emailstr = emailTB.Text.Split(new char[] { '@', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (!(emailstr[1]=="gmail" && emailstr[1]=="yahoo") && !(emailstr[2]=="com"))
                {
                    emailok = 0;
                    //new MessageBoxPoni("Wrong email format!").Show();
                }

                string firstname = FirstNameTB.Text;
                string lastname = LastNameTB.Text;

                if(firstname[0]>='A' && firstname[0] <= 'Z')
                {
                    for(int i = 1; i < firstname.Length; i++)
                    {
                        if (!(firstname[i]>='a' && firstname[i]<='z'))
                        {
                            fnok = 0;
                        }
                    }
                }
                else
                {
                    firstletterfn = 0;
                }

                if (lastname[0] >= 'A' && lastname[0] <= 'Z')
                {
                    for (int i = 1; i < lastname.Length; i++)
                    {
                        if (!(lastname[i] >= 'a' && lastname[i] <= 'z'))
                        {
                            lnok = 0;
                        }
                    }
                }
                else
                {
                    firstletterln = 0;
                }

                string s = SallaryTB.Text;
                if (s[0] == '0')// Daca salariul incepe cu cifra 0
                {
                    salaryok = 0; 
                }

                for(int i = 0; i < s.Length; i++)
                {
                    if (!(s[i]>='1' && s[i]<='9'))
                    {
                        salaryok = 0;
                    }
                }

                if (emailok==1 && firstletterfn==1 && firstletterln==1 && lnok==1 && fnok==1 && salaryok==1)
                {
                    myCon.Open();
                    cmd = new SqlCommand("INSERT INTO [user] (Email,[Password],Salary) VALUES (@Email,@Password,@Salary) ", myCon);
                    cmd.Parameters.AddWithValue("@Email", emailTB.Text);
                    cmd.Parameters.AddWithValue("@Password", PassP.Password);
                    cmd.Parameters.AddWithValue("@Salary", SallaryTB.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO [Dealer] (FirstName,LastName,Email,[Password]) VALUES (@FirstName,@LastName,@email,@Password) ", myCon);
                    cmd.Parameters.AddWithValue("@FirstName", FirstNameTB.Text);
                    cmd.Parameters.AddWithValue("@LastName", LastNameTB.Text);
                    cmd.Parameters.AddWithValue("@Email", emailTB.Text);
                    cmd.Parameters.AddWithValue("@Password", PassP.Password);
                    cmd.ExecuteNonQuery();
                    double salary = Convert.ToDouble(SallaryTB.Text);
                    utilizator = new Utilizator(emailTB.Text, PassP.Password, 0, salary);
                    utilizator.nume = LastNameTB.Text;
                    utilizator.prenume = FirstNameTB.Text;
                    utilizator.salesNumber = 0;
                }
                else
                {
                    new MessageBoxPoni("Email,FirstName,LastName or Salary fields might have a wrong format!").Show();
                    emailTB.Text = ""; PassP.Password = ""; FirstNameTB.Text = ""; LastNameTB.Text = ""; SallaryTB.Text= "";
                }

            }
            catch (IndexOutOfRangeException ex)
            {
                new MessageBoxPoni("Wrong email format!  Hint: Use example@gmail.com \n or example@yahoo.com").Show();
                Console.WriteLine(ex.Message);
                emailTB.Text = ""; PassP.Password = ""; FirstNameTB.Text = ""; LastNameTB.Text = ""; SallaryTB.Text = "";
                myCon.Close();
                return;

            }
            catch(SqlException ex)
            {
                new MessageBoxPoni("No connection to the database!").Show();
                Console.WriteLine(ex.Message);
                myCon.Close();
                emailTB.Text = ""; PassP.Password = ""; FirstNameTB.Text = ""; LastNameTB.Text = ""; SallaryTB.Text = "";
                return;
            }
            myCon.Close();
            this.Hide();
        }
        public Utilizator getUtil()
        {
            return utilizator;
        }
        private void loginTBlock_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Ellipse_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
