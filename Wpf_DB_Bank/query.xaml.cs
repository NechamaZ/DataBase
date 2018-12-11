using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OracleClient;
using System.Data;
using WpfApplication1;
using System.Text.RegularExpressions;

namespace Wpf_DB_Bank
{
    /// <summary>
    /// Interaction logic for query.xaml
    /// </summary>
    public partial class query : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter1;

        DataTable dt, dt2, dt3;

        public enum rankk
        {
            manager,
            clerk, cleaner
        };


        public query()
        {
            InitializeComponent();


            try
            {
                oracleConnection1.Open();

                //initialize oracle and non oracle objects
                dataAdapter1 = new OracleDataAdapter();
                dataAdapter1.SelectCommand = new OracleCommand();

                dt = new DataTable();
                dt2 = new DataTable();
                dt3 = new DataTable();


                //prepare data adapter for sql query
                dataAdapter1.SelectCommand.Connection = oracleConnection1;

                //מילוי קומבובוקס של דרגה
                rank.ItemsSource = Enum.GetValues(typeof(rankk)).Cast<rankk>();

                salary.MaxLength = 9;

                oracleConnection1.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void q1_Click(object sender, RoutedEventArgs e)
        {

            String r = rank.Text.ToString();

            dt.Clear();

            //prepare query
            string SQLquery = "select count(wid) as Number_Of_Workers, b.branch_name as Branch_Name from worker w, branch b where rank = '" + r + "' and salary > " + salary.Text.ToString() + " and b.bid=w.bid group by branch_name";
            dataAdapter1.SelectCommand.CommandText = SQLquery;
            dataAdapter1.Fill(dt);
            grid1.ItemsSource = dt.DefaultView;

          
            grid3.Visibility = System.Windows.Visibility.Hidden;
            grid2.Visibility = System.Windows.Visibility.Hidden;
            grid1.Visibility = System.Windows.Visibility.Visible;
        }


        private void q2_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            String SQLquery=" select * from client where cid in (select cid from account_client natural join (select acc_number from (select count(tid) as counter , acc_number  from transactions  group by acc_number) x  where counter>2))";


               // String SQLquery = "select bid,(select min(salary) from worker w where w.bid = b.bid) as minimum_salary,  (select max(salary) from worker w where w.bid = b.bid) as maximum_salary ,(select avg(salary) from worker w where w.bid = b.bid) as average_salary from branch b group by b.bid order by b.bid";
                dt2.Clear();
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt2);
                grid2.ItemsSource = dt2.DefaultView;

                grid1.Visibility = System.Windows.Visibility.Hidden;
                grid3.Visibility = System.Windows.Visibility.Hidden;
                grid2.Visibility = System.Windows.Visibility.Visible;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void q3_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                string SQLquery2 = "select first_name , last_name , cid , address from account_client natural join client  natural join  (select acc_number from credit_card  where expired_date < '"+date+"')";
                dt3.Clear();
                dataAdapter1.SelectCommand.CommandText = SQLquery2;
                dataAdapter1.Fill(dt3);
                grid3.ItemsSource = dt3.DefaultView;


                grid1.Visibility = System.Windows.Visibility.Hidden;
                grid3.Visibility = System.Windows.Visibility.Visible;
                grid2.Visibility = System.Windows.Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void salary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }

        }

    }
}
