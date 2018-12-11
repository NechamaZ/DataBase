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
using System.Data.OracleClient;
using System.Data;


namespace Wpf_DB_Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleDataAdapter dataAdapter1;
        OracleConnection oracleConnection1;
        DataTable dt;

        public MainWindow()
        {

            InitializeComponent();


            OracleConnectionStringBuilder myCStringB = new OracleConnectionStringBuilder();

        

            //myCStringB.UserID = "pirian";//put in username
            //myCStringB.Password = "308528041";//put in password
            //myCStringB.DataSource = "labdbwin";//use this for connecting to

            myCStringB.UserID = "system";//put in username
            myCStringB.Password = "nechama2301";//put in password
            myCStringB.DataSource = "XE";//use this for connecting to

            oracleConnection1 = new OracleConnection(myCStringB.ConnectionString);
            oracleConnection1.Open();

            //initialize oracle and non oracle objects
            dataAdapter1 = new OracleDataAdapter();
            dataAdapter1.SelectCommand = new OracleCommand();
            dt = new DataTable();


            // *********************** //

            dt.Clear();

            //prepare query
            string SQLquery = "select * from branch";

            //prepare data adapter for sql query
            dataAdapter1.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.CommandText = SQLquery;
            dataAdapter1.Fill(dt);
            //dataGrid.ItemsSource = dt.DefaultView;


        }

        private void Select_Click_1(object sender, RoutedEventArgs e)
        {
            Select s = new Select();
            s.ShowDialog();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clientUID c = new clientUID();
            c.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            accountUID c = new accountUID();
            c.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            branchUID b = new branchUID();
            b.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            credit_cardUID c = new credit_cardUID();
            c.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            workerUID w = new workerUID();
            w.ShowDialog();
        }

        //private void Button_Click_6(object sender, RoutedEventArgs e)
        //{
        //    transactionsUID t = new transactionsUID();
        //    t.ShowDialog();
        //}

        private void Button_MouseMove_1(object sender, MouseEventArgs e)
        {
            s.FontSize = 30;
        }

        private void Button_MouseMove_2(object sender, MouseEventArgs e)
        {
            c.FontSize = 30;
        }

        private void Button_MouseMove_3(object sender, MouseEventArgs e)
        {
            a.FontSize = 30;
        }

        private void Button_MouseMove_4(object sender, MouseEventArgs e)
        {
            b.FontSize = 30;
        }

        private void Button_MouseMove_5(object sender, MouseEventArgs e)
        {
            cc.FontSize = 30;
        }

        private void t_MouseMove(object sender, MouseEventArgs e)
        {
            t.FontSize = 30;
        }

        private void Button_MouseMove_6(object sender, MouseEventArgs e)
        {
            w.FontSize = 30;
        }

        private void s_MouseLeave(object sender, MouseEventArgs e)
        {
            s.FontSize = 24;
        }

        private void c_MouseLeave(object sender, MouseEventArgs e)
        {
            c.FontSize = 24;
        }

        private void a_MouseLeave(object sender, MouseEventArgs e)
        {
            a.FontSize = 24;
        }

        private void b_MouseLeave(object sender, MouseEventArgs e)
        {
            b.FontSize = 24;
        }

        private void cc_MouseLeave(object sender, MouseEventArgs e)
        {
            cc.FontSize = 24;
        }

        private void t_MouseLeave(object sender, MouseEventArgs e)
        {
            t.FontSize = 24;
        }

        private void w_MouseLeave(object sender, MouseEventArgs e)
        {
            w.FontSize = 24;
        }

        private void t_Click(object sender, RoutedEventArgs e)
        {
            transactionsUID t = new transactionsUID();
               t.ShowDialog();
            
        }

        private void q_Click(object sender, RoutedEventArgs e)
        {
            query t = new query();
            t.ShowDialog();
        }

        private void p_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                proc t = new proc();
                t.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


          
        }


        private void ac_Click(object sender, RoutedEventArgs e)
        {
            account_client t = new account_client();
            t.ShowDialog();
        }

       



    }
}





