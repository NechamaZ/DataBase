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


namespace Wpf_DB_Bank
{
    /// <summary>
    /// Interaction logic for account_client.xaml
    /// </summary>
    public partial class account_client : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();

        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;

        OracleDataAdapter dataAdapter1;
        OracleDataAdapter dataAdapter2;


        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();

        public account_client()
        {


            try
            {
                InitializeComponent();

                InsertCommand = new OracleCommand();
                DeleteCommand = new OracleCommand();


                dataAdapter2 = new OracleDataAdapter();
                dataAdapter2.SelectCommand = new OracleCommand();
                dataAdapter1 = new OracleDataAdapter();
                dataAdapter1.SelectCommand = new OracleCommand();

                string datee = DateTime.Now.ToString("dd/MM/yyyy");

                //prepare query
                string SQLquery = "select cid from client";
                string SQLquery2 = "select acc_number from account where open_date= '" +datee + "'";

                //prepare data adapter for sql query
                dataAdapter2.SelectCommand.Connection = oracleConnection1;
                dataAdapter2.SelectCommand.CommandText = SQLquery;
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = SQLquery2;

                dataAdapter2.Fill(ds, "client");
                cid_combo.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();

                dataAdapter1.Fill(ds2, "account");
                acc_combo.ItemsSource = ds2.Tables[0].DefaultView;
                acc_combo.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

                //dataAdapter1.Fill(ds2, "account_client");
                //acc_combo3.ItemsSource = ds2.Tables[0].DefaultView;
                //acc_combo3.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();


                oracleConnection1.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }



        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            
            DataTable dt = new DataTable();
            
            dataAdapter1.SelectCommand.Connection = oracleConnection1;
            string acc = acc_combo.Text.ToString();
            dataAdapter1.SelectCommand.CommandText = "select bid from account where acc_number='" + acc + "'";
            dataAdapter1.Fill(dt);

            string b = dt.Rows[0]["bid"].ToString();


            InsertCommand.CommandText = " insert into account_client (cid, acc_number, bid) values (" + "'" +
             this.cid_combo.Text + "','" + acc_combo.Text + "','" + b + "')";


            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Account - client added succesfuly");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();

        }

        private void Update(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
