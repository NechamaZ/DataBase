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
    /// Interaction logic for credit_cardUID.xaml
    /// </summary>
    public partial class credit_cardUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter3;
        OracleDataAdapter dataAdapter2;
        OracleDataAdapter dataAdapter1;

        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataTable dt = new DataTable();


        public credit_cardUID()
        {
            InitializeComponent();
            oracleConnection1.Open();

            //string datee = DateTime.Now.ToString("dd/MM/yyyy");
            //this.date.Content = datee; // תאריך של אותו יום

            //הגבלת אורכי קלט
          
            limit.MaxLength = 20;
            limit1.MaxLength = 20;



            tc.SelectedIndex = 0;

            InsertCommand = new OracleCommand();
            DeleteCommand = new OracleCommand();
            UpdateCommand = new OracleCommand();

            dataAdapter3 = new OracleDataAdapter();
            dataAdapter2 = new OracleDataAdapter();
            dataAdapter1 = new OracleDataAdapter();

            dataAdapter3.SelectCommand = new OracleCommand();
            dataAdapter2.SelectCommand = new OracleCommand();
            dataAdapter1.SelectCommand = new OracleCommand();

            run_bid();

            //prepare query
            string SQLquery = "select bid from branch";
            string SQLquery2 = "select acc_number from account";
            string SQLquery3 = "select credit_number from credit_card";

            //prepare data adapter for sql query
            dataAdapter3.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.Connection = oracleConnection1;

            dataAdapter2.SelectCommand.CommandText = SQLquery;
            dataAdapter1.SelectCommand.CommandText = SQLquery2;
            dataAdapter3.SelectCommand.CommandText = SQLquery3;

            dataAdapter2.Fill(ds, "brnach");
            bid_combo.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            dataAdapter1.Fill(ds2, "account");
            acc_combo.ItemsSource = ds2.Tables[0].DefaultView;
            acc_combo.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

            acc_combo1.ItemsSource = ds2.Tables[0].DefaultView;
            acc_combo1.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

            dataAdapter3.Fill(ds3, "credit_card");
            card_combo1.ItemsSource = ds3.Tables[0].DefaultView;
            card_combo1.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

            card_combo2.ItemsSource = ds3.Tables[0].DefaultView;
            card_combo2.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

            oracleConnection1.Close();
        }


        private void run_bid() /////////////////////////////
        {

            if (tc.SelectedIndex == 0) // insert window
            {
                dt.Clear();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = "select max(credit_number)+1 as credit_number  from credit_card";
                dataAdapter1.Fill(dt);
                card.Text = dt.Rows[0]["credit_number"].ToString();
            }

        }

        // insert account button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            String Value = acc_combo.Text;
            String Value1 = bid_combo.Text;

            String d = "";
            DateTime? selectedDate = date.SelectedDate;
            if (selectedDate.HasValue)
            {
                d = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            //InsertCommand.CommandText = " insert into credit_card (credit_number, expired_date, limit, acc_number, bid) values (" + "'" +
            //   this.card.Text + "'" + ","  + "to_date('" + expdate.Text + "', 'DD/MM/YYYY')"  + "," + "'" + this.limit.Text + "'" + "," + "'" + Value + "'" + ",'" + Value1 + "')";

            InsertCommand.CommandText = " insert into credit_card (credit_number, expired_date, limit, acc_number, bid) values (" + "'" +
              this.card.Text + "','" +d + "','" + this.limit.Text + "'" + "," + "'" + Value + "'" + ",'" + Value1 + "')";
            
            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("credit-card added succesfuly");

                ds3.Clear();

                dataAdapter3.Fill(ds3, "credit_card");
                card_combo1.ItemsSource = ds3.Tables[0].DefaultView;
                card_combo1.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

                card_combo2.ItemsSource = ds3.Tables[0].DefaultView;
                card_combo2.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

                run_bid();
                // close ?
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }


        // delete account button
        private void Delete(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            DeleteCommand.Connection = oracleConnection1;

            String Value = card_combo2.Text;

            DeleteCommand.CommandText = " delete from credit_card where " + "credit_number=" + "'" + Value + "'"; // אולי צריך נקודה פסיק או COMMIT ?

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("credit-number deleted succesfuly");

                ds3.Clear();

                dataAdapter3.Fill(ds3, "credit_card");
                card_combo1.ItemsSource = ds3.Tables[0].DefaultView;
                card_combo1.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

                card_combo2.ItemsSource = ds3.Tables[0].DefaultView;
                card_combo2.DisplayMemberPath = ds3.Tables[0].Columns["credit_number"].ToString();

                run_bid();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }



        // update account button
        private void Update(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            UpdateCommand.Connection = oracleConnection1;


            String card = card_combo1.Text;
            String acc = acc_combo1.Text;
            String bid = bid_combo1.Text;

            // האם לאפשר בעדכון- לעדכן את מספר החשבון המשוייך לכרטיס האשראי שנבחר ?????????///

            //UpdateCommand.CommandText = " update credit_card set expired_date='" + expdate1.Text + "'" + ", limit='"
            //   + limit1.Text + "',bid='"+ bid+"',acc_number='"+ acc+"' where credit_number='" + card + "'";

            String d="";
            DateTime? selectedDate = date1.SelectedDate;
            if (selectedDate.HasValue)
            {
             d = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            UpdateCommand.CommandText = " update credit_card set expired_date='" + d + "'" + ", limit='"
            + limit1.Text + "',bid='" + bid + "',acc_number='" + acc + "' where credit_number='" + card + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("credit card updated succesfuly");

                run_bid();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }


        private void card_combo1_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String card = card_combo1.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from credit_card where credit_number='" + card + "'";
                dataAdapter1.Fill(dt);

                acc_combo1.Text = dt.Rows[0]["acc_number"].ToString();
                bid_combo1.Text = dt.Rows[0]["bid"].ToString();
                date1.Text = dt.Rows[0]["expired_date"].ToString();
                limit1.Text = dt.Rows[0]["limit"].ToString();

            }


            catch (Exception ex)
            {
                MessageBox.Show("You didn't select any credit number");
            }

            oracleConnection1.Close();
        }

        private void limit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
             
                e.Handled = true;
            }
        }

        private void limit1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {

                e.Handled = true;
            }
        }



    }
}