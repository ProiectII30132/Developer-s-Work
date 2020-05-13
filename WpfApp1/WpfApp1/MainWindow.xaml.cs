﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace WpfApp1
{
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    SqlCommand cmd;
    SqlConnection myCon = new SqlConnection();


    public MainWindow()
    {
        InitializeComponent();
            try
            {
                StreamReader streamReader = new StreamReader("remember_me.txt");
                String linie;
                if ((linie = streamReader.ReadLine()) != null)
                {
                    String[] emailsiparola = linie.Split(' ');
                    emailTextBox.Text = emailsiparola[0];
                    passwordPasswoerdBox.Password = emailsiparola[1];

                }
                streamReader.Close();

            }
            catch (IOException ex)
            {

            }

        }

   private void loginButton_Click(object sender, RoutedEventArgs e)
      {

        List<Utilizator> utilizatori = new List<Utilizator>();
        string email = emailTextBox.Text;
        string pass = passwordPasswoerdBox.Password.ToString();
        myCon.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ii-proj\Desginer-s_Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True";
        myCon.Open();

        DataSet dataset = new DataSet();
        try
        {
            if (!email.Equals("") && !pass.Equals("") && Remember_me.IsChecked == true)
            {
                StreamWriter streamWriter = new StreamWriter("remember_me.txt");
                streamWriter.WriteLine(email + " " + pass);
                streamWriter.Close();
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show(ex.Message);
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [user]", myCon);
        dataAdapter.Fill(dataset, "[user]");
        foreach (DataRow dr in dataset.Tables["[user]"].Rows)
        {
            int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
            String emailRead = dr.ItemArray.GetValue(1).ToString();
            String passRead = dr.ItemArray.GetValue(2).ToString();
            utilizatori.Add(new Utilizator(emailRead, passRead, admin));
            
        }
        myCon.Close();
        bool merge = false;
        foreach(Utilizator utilizator in utilizatori)
            {
                if(utilizator.email == email && utilizator.pass==pass)
                {
                    UserMenu userMenu = new UserMenu(utilizator);
                    userMenu.Show();
                    this.Close();
                    merge = true;
                }
            }
            
         if(merge == false)
            {
                new MessageBoxPoni("Parola gresita").Show();
                emailTextBox.Text = "";
                passwordPasswoerdBox.Password = "";
            }


    }

    private void fpTextBlock_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        Fpas fpas = new Fpas();
        this.Close();
        fpas.Show();
    }

    private void signUpTBlock_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        SignUp signup = new SignUp();
        this.Close();
        signup.Show();
    }

    private void Ellipse_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        Environment.Exit(1);

    }

    private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        new MessageBoxPoni("Welcome! \n Te saluta CAS").Show();
    }
}
}