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
using WpfApplication1;
using System.Data;
using System.Text.RegularExpressions;


namespace Wpf_DB_Bank
{
    /// <summary>
    /// Interaction logic for transactionsUID.xaml
    /// </summary>
    public partial class transactionsUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
  
        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        OracleDataAdapter dataAdapter1;
        OracleDataAdapter dataAdapter2;
        OracleDataAdapter dataAdapter3;

        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataTable dt = new DataTable();

        public enum type
        {
            withdrawal,
            loan, deposit, transfer
        };


        public transactionsUID()
        {
            InitializeComponent();
            oracleConnection1.Open();

            tc.SelectedIndex = 0;

            InsertCommand = new OracleCommand();
            DeleteCommand = new OracleCommand();
            UpdateCommand = new OracleCommand();

            dataAdapter3 = new OracleDataAdapter();
            dataAdapter2 = new OracleDataAdapter();
            dataAdapter1 = new OracleDataAdapter();

            dataAdapter1.SelectCommand = new OracleCommand();
            dataAdapter2.SelectCommand = new OracleCommand();
            dataAdapter3.SelectCommand = new OracleCommand();

            //prepare query
            string SQLquery = "select acc_number from account";
            string SQLquery1 = "select bid from branch";
            string SQLquery2 = "select tid from transactions";

            //prepare data adapter for sql query
            dataAdapter3.SelectCommand.Connection = oracleConnection1;
            dataAdapter3.SelectCommand.CommandText = SQLquery2;
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.CommandText = SQLquery1;
            dataAdapter1.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.CommandText = SQLquery;

            //אתחול קומבו בוקס של סוג 
            type_combo3.ItemsSource = Enum.GetValues(typeof(type)).Cast<type>();
            type_combo4.ItemsSource = Enum.GetValues(typeof(type)).Cast<type>();


            dataAdapter1.Fill(ds, "account");
            acc_combo3.ItemsSource = ds.Tables[0].DefaultView;
            acc_combo3.DisplayMemberPath = ds.Tables[0].Columns["acc_number"].ToString();
            trans_combo4.ItemsSource = ds.Tables[0].DefaultView;
            trans_combo4.DisplayMemberPath = ds.Tables[0].Columns["acc_number"].ToString();
            trans_combo3.ItemsSource = ds.Tables[0].DefaultView;
            trans_combo3.DisplayMemberPath = ds.Tables[0].Columns["acc_number"].ToString();

            dataAdapter2.Fill(ds2, "branch");
            // for insert
            bid_combo3.ItemsSource = ds2.Tables[0].DefaultView;
            bid_combo3.DisplayMemberPath = ds2.Tables[0].Columns["bid"].ToString();


            dataAdapter3.Fill(ds3, "transactions");
            transID_combo.ItemsSource = ds3.Tables[0].DefaultView;
            transID_combo.DisplayMemberPath = ds3.Tables[0].Columns["tid"].ToString();
            transID_combo4.ItemsSource = ds3.Tables[0].DefaultView;
            transID_combo4.DisplayMemberPath = ds3.Tables[0].Columns["tid"].ToString();
      
   
       
            oracleConnection1.Close();
        }


        // insert branch button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            
           // InsertCommand.CommandText = " insert into branch (branch_name, address, city, tellephone, close_hour, open_hour, bid) values (" + "'" +
             //  this.name.Text + "'" + "," + "'" + this.address.Text + "'" + "," + "'" + this.city.Text + "'" + "," + "'" + this.tell.Text + "'" + "," + "'" + this.close.Text + "'" + "," + "'" + this.open.Text + "'" +",'" + this.bid.Text + "' )";

            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("branch added succesfuly");

                ds.Clear();

                //dataAdapter2.Fill(ds, "branch");
                //bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                //bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                // close ?
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }


        // delete branch button
        private void Delete(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            DeleteCommand.Connection = oracleConnection1;

            // if (cid_combo.SelectedIndex >= 0)

           // String Value = bid_combo1.Text;

            //DeleteCommand.CommandText = " delete from branch where " + "bid=" + "'" + Value + "'"; // אולי צריך נקודה פסיק או COMMIT ?

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("branch deleted succesfuly");

                ds.Clear();

                //dataAdapter2.Fill(ds, "branch");
                //bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                //bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();
                                    
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }



        // update branch button
        private void Update(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            UpdateCommand.Connection = oracleConnection1;

      
            //String Value = bid_combo.Text;


            //UpdateCommand.CommandText = " update branch set branch_name='" + name1.Text + "'" + ",address='" + address1.Text + "'" + ", city='"
            //    + city1.Text + "'" + ", tellephone='" + address1.Text + "'" + ", close_hour='" + close1.Text + "'" + ", open_hour='" + open1.Text + "'" + " where bid='" + Value + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("branch updated succesfuly");

                ds.Clear();

                //dataAdapter2.Fill(ds, "branch");
                //bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                //bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                //bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                                                     }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }


        private void transID_combo4_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String tid = transID_combo4.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from transactions where tid='" + tid + "'";
                dataAdapter1.Fill(dt);

                amount1.Text = dt.Rows[0]["amount"].ToString();
                type_combo4.Text = dt.Rows[0]["type"].ToString();
                date1.Text = dt.Rows[0]["date_time"].ToString();
                trans_combo4.Text = dt.Rows[0]["transfer_to"].ToString();
                
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();
        }


    

    }
}
