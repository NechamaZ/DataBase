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
    /// Interaction logic for branchUID.xaml
    /// </summary>
    public partial class branchUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter2;
        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        OracleDataAdapter dataAdapter1;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        //DataTable dt;



        public branchUID()
        {
            InitializeComponent();
            oracleConnection1.Open();

            //הגבלת אורכי קלט

            name.MaxLength = 30;
            tell.MaxLength = 10;
            address.MaxLength = 25;
            city.MaxLength = 30;
            bid.MaxLength = 8;

            name1.MaxLength = 30;
            tell1.MaxLength = 10;
            address1.MaxLength = 25;
            city1.MaxLength = 30;



            tc.SelectedIndex = 0;

            InsertCommand = new OracleCommand();
            DeleteCommand = new OracleCommand();
            UpdateCommand = new OracleCommand();

            dataAdapter2 = new OracleDataAdapter();

            dataAdapter1 = new OracleDataAdapter();
            dataAdapter1.SelectCommand = new OracleCommand();



            /////////// צריך גם InsertCommand ????? 
            dataAdapter2.SelectCommand = new OracleCommand();

            //dt = new DataTable();


            dt.Clear();

            run_bid(); ////////////////////

            //prepare query
            string SQLquery = "select bid from branch";

            //prepare data adapter for sql query
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.CommandText = SQLquery;


            dataAdapter2.Fill(ds, "branch");
            bid_combo.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
            bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

            oracleConnection1.Close();
        }

        private void run_bid() /////////////////////////////
        {

            if (tc.SelectedIndex == 0) // insert window
            {
                dt.Clear();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = "select max(bid)+1 as bid from branch";
                dataAdapter1.Fill(dt);
                bid.Text = dt.Rows[0]["bid"].ToString();
            }

        }

        // insert branch button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            //תקינות שהשעות נכונות והגיוניות
            if (DateTime.Parse(open.Text) > DateTime.Parse(close.Text))
            {
                MessageBox.Show("Error \n Cannot add open hour bigger than close hour.");
                open.Text = "";
                close.Text = "";
                return;
            }


            InsertCommand.CommandText = " insert into branch (branch_name, address, city, tellephone, close_hour, open_hour, bid) values (" + "'" +
               this.name.Text + "'" + "," + "'" + this.address.Text + "'" + "," + "'" + this.city.Text + "'" + "," + "'" + this.tell.Text + "'" + "," + "'" + this.close.Text + "'" + "," + "'" + this.open.Text + "'" + ",'" + this.bid.Text + "' )";

            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("branch added succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "branch");
                bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                run_bid(); ////////////////////

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

            String Value = bid_combo1.Text;

            DeleteCommand.CommandText = " delete from branch where " + "bid=" + "'" + Value + "'";

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("branch deleted succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "branch");
                bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                run_bid(); ////////////////////

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


            String Value = bid_combo.Text;


            UpdateCommand.CommandText = " update branch set branch_name='" + name1.Text + "'" + ",address='" + address1.Text + "'" + ", city='"
                + city1.Text + "'" + ", tellephone='" + tell1.Text + "'" + ", close_hour='" + close1.Text + "'" + ", open_hour='" + open1.Text + "'" + " where bid='" + Value + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("branch updated succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "branch");
                bid_combo.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                bid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                bid_combo1.DisplayMemberPath = ds.Tables[0].Columns["bid"].ToString();

                run_bid(); ////////////////////

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }



        private void open_LostFocus(object sender, RoutedEventArgs e)
        {
            string time = open.Text;

            DateTime dt;

            if (!(DateTime.TryParseExact(time, "HH:mm",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None, out dt)))
            {
                MessageBox.Show("Error. \n Time is not in format HH:mm ");
                open.Text = "";
            }

        }

        private void close_LostFocus(object sender, RoutedEventArgs e)
        {
            string time = close.Text;

            DateTime dt;

            if (!(DateTime.TryParseExact(time, "HH:mm",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None, out dt)))
            {
                MessageBox.Show("Error. \n Time is not in format HH:mm ");
                close.Text = "";
            }
        }

        private void open1_LostFocus(object sender, RoutedEventArgs e)
        {
            string time = open1.Text;

            DateTime dt;

            if (!(DateTime.TryParseExact(time, "HH:mm",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None, out dt)))
            {
                MessageBox.Show("Error. \n Time is not in format HH:mm ");
                open1.Text = "";
            }
        }

        private void close1_LostFocus(object sender, RoutedEventArgs e)
        {
            string time = close1.Text;

            DateTime dt;

            if (!(DateTime.TryParseExact(time, "HH:mm",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None, out dt))) 
            {
                MessageBox.Show("Error. \n Time is not in format HH:mm ");
                close1.Text = "";
            }
        }





        private void bid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }
        }

        private void tell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            // Regex regex = new Regex("^[0-9]{2}[-]{1}[0-9]{7}$");
            //// if (Regex.IsMatch(e.Text, "^(?[0-9]{2})?[s-]?[0-9]{7}$") == true)

            // if (!regex.IsMatch(e.Text) == true)
            // {
            //     MessageBox.Show(".אנא הכנס רק ספרות, ללא מקף ");
            //     e.Handled = true;
            // }
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }


        }

        private void bid_combo_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String bid = bid_combo.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from branch where bid='" + bid + "'";
                dataAdapter1.Fill(dt);

                name1.Text = dt.Rows[0]["branch_name"].ToString();
                address1.Text = dt.Rows[0]["address"].ToString();
                city1.Text = dt.Rows[0]["city"].ToString();
                tell1.Text = dt.Rows[0]["tellephone"].ToString();
                open1.Text = dt.Rows[0]["open_hour"].ToString();
                close1.Text = dt.Rows[0]["close_hour"].ToString();

            }


            catch (Exception ex)
            {
                MessageBox.Show("You didn't select any branch ID");
            }

            oracleConnection1.Close();
        }


        public bool CheckOnlyText(string e)
        {
            for (int i = 0; i < e.Length; i++)
                if (e[i] < 'a' || e[i] > 'z')//|| e[i] > 'A' || e[i] < 'Z')
                    return true;
            return false;
        }

        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void city_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void name1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void city1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void tell1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tell1_LostFocus(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות אורך מספר פלאפון

            if (tell1.Text.Count() != 10)
            {
                MessageBox.Show("נא להכניס מספר טלפון תקין עם מקף, בעל 10 ספרות");

            }

        }



        private void tell_LostFocus(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות אורך מספר פלאפון

            if (tell.Text.Count() != 10)
            {
                MessageBox.Show("נא להכניס מספר טלפון תקין עם מקף, בעל 10 ספרות");

            }

        }


    }
}
