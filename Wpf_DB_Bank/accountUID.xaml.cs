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
    /// Interaction logic for accountUID.xaml
    /// </summary>
    ///
    public partial class accountUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter2;
        OracleDataAdapter dataAdapter1;

        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
       

        DataTable dt = new DataTable();


        public accountUID()
        {
            InitializeComponent();
            oracleConnection1.Open();

            string datee = DateTime.Now.ToString("dd/MM/yyyy");
            this.date.Content = datee; // תאריך של אותו יום

            //הגבלת אורכי קלט
            accnum.MaxLength = 6;
            limit.MaxLength = 20;
            balance.MaxLength = 20;

            limit1.MaxLength = 20;
            balance1.MaxLength = 20;

            dataAdapter2 = new OracleDataAdapter();
            dataAdapter1 = new OracleDataAdapter();
            dataAdapter2.SelectCommand = new OracleCommand();
            dataAdapter1.SelectCommand = new OracleCommand();

            tc.SelectedIndex = 0;

           
            //if (tc.SelectedIndex == 0)
            //{
            //    dt.Clear();
            //    dataAdapter1.SelectCommand.Connection = oracleConnection1;
            //    dataAdapter1.SelectCommand.CommandText = "select max(acc_number)+1 as acc from account";
            //    dataAdapter1.Fill(dt);
            //    accnum.Text = dt.Rows[0]["acc"].ToString();
            //}

            InsertCommand = new OracleCommand();
            DeleteCommand = new OracleCommand();
            UpdateCommand = new OracleCommand();




            dt.Clear();

         

            run_acc(); ////////////////////

            //prepare query
            string SQLquery = "select bid,branch_name from branch";
            string SQLquery2 = "select acc_number from account";

            //prepare data adapter for sql query
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.Connection = oracleConnection1;

            dataAdapter2.SelectCommand.CommandText = SQLquery;
            dataAdapter1.SelectCommand.CommandText = SQLquery2;

            dataAdapter2.Fill(ds, "brnach");
            bid_combo.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            bid_combo2.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo2.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            dataAdapter1.Fill(ds2, "account");
            acc_combo.ItemsSource = ds2.Tables[0].DefaultView;
            acc_combo.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

            acc_combo2.ItemsSource = ds2.Tables[0].DefaultView;
            acc_combo2.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

            oracleConnection1.Close();
        }

        private void run_acc() /////////////////////////////
        {

            if (tc.SelectedIndex == 0) // insert window
            {
                dt.Clear();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = "select max(acc_number)+1 as acc from account";
                dataAdapter1.Fill(dt);
                accnum.Text = dt.Rows[0]["acc"].ToString();
            }


        }


        // insert account button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            String Value = bid_combo.Text;

            string typ;

            if (this.p.IsChecked == true)
                typ = "Private";
            else
                typ = "Business";

            //string datee = DateTime.Now.ToString("DD/MM/YYYY");



            InsertCommand.CommandText = " insert into account (acc_number, bid, type, limit, open_date, balance) values (" + "'" +
             accnum.Text + "'" + "," + "'" + Value + "'" + "," + "'" + typ + "'" + "," + "'" + this.limit.Text + "'" + "," + "to_date('" + date.Content + "', 'DD/MM/YYYY')" /*+ this.open.Text + "'"*/ + "," + this.balance.Text/*"'" + this.balance.Text + "'" */+ " )";

            InsertCommand.CommandText = " insert into account (acc_number, bid, type, limit, open_date, balance) values (" + "'" +
           accnum.Text + "'" + "," + "'" + Value + "'" + "," + "'" + typ + "'" + "," + "'" + this.limit.Text + "'" + ",'" + date.Content +"'," + this.balance.Text/*"'" + this.balance.Text + "'" */+ " )";


            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("account added succesfuly");

                ds2.Clear();

                string SQLquery2 = "select acc_number from account";
                dataAdapter1.SelectCommand.CommandText = SQLquery2;

                dataAdapter1.Fill(ds2, "account");
                acc_combo.ItemsSource = ds2.Tables[0].DefaultView;
                acc_combo.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

                acc_combo2.ItemsSource = ds2.Tables[0].DefaultView;
                acc_combo2.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

                run_acc(); /////////////////////

           
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

            String Value = acc_combo.Text;

            DeleteCommand.CommandText = " delete from account where " + "acc_number=" + "'" + Value + "'"; // אולי צריך נקודה פסיק או COMMIT ?

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("account deleted succesfuly");

                ds2.Clear();

                string SQLquery2 = "select acc_number from account";
                dataAdapter1.SelectCommand.CommandText = SQLquery2;

                dataAdapter1.Fill(ds2, "account");
                acc_combo.ItemsSource = ds2.Tables[0].DefaultView;
                acc_combo.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();
     
                acc_combo2.ItemsSource = ds2.Tables[0].DefaultView;
                acc_combo2.DisplayMemberPath = ds2.Tables[0].Columns["acc_number"].ToString();

                run_acc(); //////////////////////

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

            string typ;

            if (this.p1.IsChecked == true)
                typ = "Private";
            else
                typ = "Business";

            String acc = acc_combo2.Text;
            String bid = bid_combo2.Text;


            UpdateCommand.CommandText = " update account set bid='" + bid + "'" + ",type='" + typ + "'" + ", limit='"
                + limit1.Text + "'" + ", balance='" + balance1.Text + "'" + " where acc_number='" + acc + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("account updated succesfuly");

                run_acc();//////////


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }

        private void accnum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("^[0-9]");

            //if (!regex.IsMatch(e.Text) == true)
            //{
            //    MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
            //    e.Handled = true;
            //}
        }

        private void limit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }
        }

        private void balance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
          
            Regex regex = new Regex("^[.][0-9]+$|^[-]{0,1}[0-9]*[.]{0,1}[0-9]*$");
           
            if (!regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text)) == true)
            {
                MessageBox.Show("אנא הכנס רק מספרים או שברים תקינים");
                e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            }

        }

        private void limit1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }

        }

        private void balance1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("^[.][0-9]+$|^[-]{0,1}[0-9]*[.]{0,1}[0-9]*$");

            if (!regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text)) == true)
            {
                MessageBox.Show("אנא הכנס רק מספרים או שברים תקינים");
                e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            }
          
        }

   

        private void acc_combo2_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String acc = acc_combo2.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from account where acc_number='" + acc + "'";
                dataAdapter1.Fill(dt);

                balance1.Text = dt.Rows[0]["balance"].ToString();
                bid_combo2.Text = dt.Rows[0]["bid"].ToString();
                limit1.Text = dt.Rows[0]["limit"].ToString();
                string ty = dt.Rows[0]["type"].ToString();


                if (ty == "Private")
                {
                    p1.IsChecked = true;
                    b1.IsChecked = false;

                }
                else if (ty == "Business")
                {
                    b1.IsChecked = true;
                    p1.IsChecked = false;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("You didn't select any account number");
            }

            oracleConnection1.Close();
        }










    }
}
