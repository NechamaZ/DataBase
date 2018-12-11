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
    /// Interaction logic for workerUID.xaml
    /// </summary>
    public partial class workerUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();

        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        OracleDataAdapter dataAdapter1;
        OracleDataAdapter dataAdapter2;

        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();

        DataTable dt;

        public enum rankk
        {
            manager,
            clerk, cleaner
        };


        public workerUID()
        {
 
            InitializeComponent();
            oracleConnection1.Open();

            //הגבלת אורכי קלט
            address.MaxLength = 25;
            phone.MaxLength = 10;
            first.MaxLength = 25;
            last.MaxLength = 25;
            wid.MaxLength = 9;
            salary.MaxLength = 9;

            address1.MaxLength = 25;
            phone1.MaxLength = 10;
            first1.MaxLength = 25;
            last1.MaxLength = 25;
            salary1.MaxLength = 9;

            tc.SelectedIndex = 0;

            InsertCommand = new OracleCommand();
            DeleteCommand = new OracleCommand();
            UpdateCommand = new OracleCommand();

            dataAdapter2 = new OracleDataAdapter();
            dataAdapter1 = new OracleDataAdapter();

            dataAdapter1.SelectCommand = new OracleCommand();
            dataAdapter2.SelectCommand = new OracleCommand();


            dt = new DataTable();

            dt.Clear();
            run_bid();//////////////

            //prepare query
            string SQLquery = "select wid from worker";
            string SQLquery1 = "select bid from branch";


            //prepare data adapter for sql query
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.CommandText = SQLquery1; // branch
            dataAdapter1.SelectCommand.Connection = oracleConnection1;
            dataAdapter1.SelectCommand.CommandText = SQLquery; // worker

            //אתחול קומבו בוקס של דרגה rank
            rank.ItemsSource = Enum.GetValues(typeof(rankk)).Cast<rankk>();
            rank1.ItemsSource = Enum.GetValues(typeof(rankk)).Cast<rankk>();

        

            dataAdapter1.Fill(ds, "worker");
            // for delete
            wid_combo2.ItemsSource = ds.Tables[0].DefaultView;
            wid_combo2.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();
            // for update
            wid_combo1.ItemsSource = ds.Tables[0].DefaultView;
            wid_combo1.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();

            dataAdapter2.Fill(ds2, "branch");

            // for insert
            bid_combo.ItemsSource = ds2.Tables[0].DefaultView;
            bid_combo.DisplayMemberPath = ds2.Tables[0].Columns["bid"].ToString();
            // for update
            bid_combo1.ItemsSource = ds2.Tables[0].DefaultView;
            bid_combo1.DisplayMemberPath = ds2.Tables[0].Columns["bid"].ToString();


            oracleConnection1.Close();
        }


        private void run_bid() /////////////////////////////
        {

            if (tc.SelectedIndex == 0) // insert window
            {
                dt.Clear();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = "select max(wid)+1 as wid from worker";
                dataAdapter1.Fill(dt);
                wid.Text = dt.Rows[0]["wid"].ToString();
            }

        }

        // insert worker button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            char gend;

            if (this.f.IsChecked == true)
                gend = 'F';
            else
                gend = 'M';

            String r = rank.Text;
            String b = bid_combo.Text;


            InsertCommand.CommandText = " insert into worker (wid,first_name, last_name,rank, salary, gender, address, phone, bid) values ('" + wid.Text + "'," + "'" +
               this.first.Text + "'" + "," + "'" + this.last.Text + "'" + "," + "'" + r + "'" + "," + "'" + salary.Text + "'" + "," + "'" + gend + "'" + "," + "'" + this.address.Text + "'" + "," + "'" + this.phone.Text + "'" + "," + "'" + b + "'" + " )";

            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("worker added succesfuly");

                ds.Clear();

                string SQLquery = "select wid from worker";
                dataAdapter1.SelectCommand.CommandText = SQLquery; // worker
                dataAdapter1.Fill(ds, "worker");
                // for delete
                wid_combo2.ItemsSource = ds.Tables[0].DefaultView;
                wid_combo2.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();
                // for update
                wid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                wid_combo1.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();

                run_bid(); ////////////////////
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }


        // delete worker button
        private void Delete(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            DeleteCommand.Connection = oracleConnection1;

            String Value = wid_combo2.Text;

            DeleteCommand.CommandText = " delete from worker where " + "wid=" + "'" + Value + "'"; // אולי צריך נקודה פסיק או COMMIT ?

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("worker deleted succesfuly");

                ds.Clear();

                string SQLquery = "select wid from worker";
                dataAdapter1.SelectCommand.CommandText = SQLquery; // worker

                dataAdapter1.Fill(ds, "worker");
                // for delete
                wid_combo2.ItemsSource = ds.Tables[0].DefaultView;
                wid_combo2.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();
                // for update
                wid_combo1.ItemsSource = ds.Tables[0].DefaultView;
                wid_combo1.DisplayMemberPath = ds.Tables[0].Columns["wid"].ToString();

                run_bid(); ////////////////////

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }



        // update worker button
        private void Update(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            UpdateCommand.Connection = oracleConnection1;

            char gend;

            if (this.f1.IsChecked == true)
                gend = 'F';
            else
                gend = 'M';

            String r = rank1.Text;
            String b = bid_combo1.Text;
            String w = wid_combo1.Text;

            UpdateCommand.CommandText = " update worker set first_name='" + first1.Text + "'" + ",last_name='" + last1.Text + "'" + ", gender='"
                + gend + "'" + ", address='" + address1.Text + "'" + ", phone='" + phone1.Text + "'" + ", salary='" + salary1.Text + "'"
                + ", rank='" + r + "'" + ", bid='" + b + "'" + " where wid='" + w + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("worker updated succesfuly");

                run_bid(); ////////////////////

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }


        private void phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show(".אנא הכנס רק ספרות, ללא מקף ");
                e.Handled = true;
            }
        }

        private void wid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            address.Text = "";
            phone.Text = "";
            first.Text = "";
            last.Text = "";
            salary.Text = "";
            wid.Text = "";
            bid_combo.SelectedValue = "";
            rank.SelectedValue = "";
           
        }

        private void Clear1(object sender, RoutedEventArgs e)
        {
            address1.Text = "";
            phone1.Text = "";
            first1.Text = "";
            last1.Text = "";
            salary1.Text = "";
            bid_combo1.SelectedValue = "";
            rank1.SelectedValue = "";
            wid_combo1.SelectedValue = "";
        }

      

        private void phone_LostFocus(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות אורך מספר פלאפון

            if (phone.Text.Count() != 10)
            {
                MessageBox.Show("נא להכניס מספר נייד תקין, בעל 10 ספרות");

                phone.Text = "";
                pho.Foreground = Brushes.Red;
            }
            else // חוזר לצבע השחור
            {
                pho.Foreground = Brushes.Black;
            }
        }

        private void phone1_LostFocus(object sender, RoutedEventArgs e)
        {
            // בדיקת תקינות אורך מספר פלאפון

            if (phone1.Text.Count() != 10)
            {
                MessageBox.Show("נא להכניס מספר נייד תקין, בעל 10 ספרות");

                phone1.Text = "";
                pho1.Foreground = Brushes.Red;
            }
            else // חוזר לצבע השחור
            {
                pho1.Foreground = Brushes.Black;
            }
        }



        private void wid_combo1_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String wid = wid_combo1.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from worker where wid='" + wid + "'";
                dataAdapter1.Fill(dt);

                bid_combo1.Text = dt.Rows[0]["bid"].ToString();
                first1.Text = dt.Rows[0]["first_name"].ToString();
                last1.Text = dt.Rows[0]["last_name"].ToString();
                address1.Text = dt.Rows[0]["address"].ToString();
                phone1.Text = dt.Rows[0]["phone"].ToString();
                rank1.Text = dt.Rows[0]["rank"].ToString();
                salary1.Text = dt.Rows[0]["salary"].ToString();
                string ge = dt.Rows[0]["gender"].ToString();


                if (ge == "F")
                {
                    f1.IsChecked = true;
                    m1.IsChecked = false;

                }
                else if (ge == "M")
                {
                    m1.IsChecked = true;
                    f1.IsChecked = false;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("You didn't select any worker ID");
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

        private void first_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void last_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void salary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");

            if (!regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text)) == true)
            {
                MessageBox.Show("אנא הכנס רק מספרים או שברים תקינים");
                e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            }
        }

        private void first1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void last1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void salary1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");

            if (!regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text)) == true)
            {
                MessageBox.Show("אנא הכנס רק מספרים או שברים תקינים");
                e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
            }
        }


    }



}