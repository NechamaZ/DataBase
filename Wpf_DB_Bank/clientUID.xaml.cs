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
    /// Interaction logic for clientUID.xaml
    /// </summary>
    public partial class clientUID : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter2;
        OracleCommand InsertCommand;
        OracleCommand DeleteCommand;
        OracleCommand UpdateCommand;

        OracleDataAdapter dataAdapter1;

        DataTable dt;

        DataSet ds = new DataSet();

        public clientUID()
        {
            InitializeComponent();
            oracleConnection1.Open();

            //הגבלת אורכי קלט
            address.MaxLength = 25;
            phone.MaxLength = 10;
            first.MaxLength = 25;
            last.MaxLength = 25;
            cid.MaxLength = 9;

            address1.MaxLength = 25;
            phone1.MaxLength = 10;
            first1.MaxLength = 25;
            last1.MaxLength = 25;

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

            run_cid(); ////////////////////

            //prepare query
            string SQLquery = "select cid,first_name from client";

            //prepare data adapter for sql query
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.CommandText = SQLquery;


            dataAdapter2.Fill(ds, "client");
            cid_combo.ItemsSource = ds.Tables[0].DefaultView;
            cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
            cid_combo.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

            cid_combo2.ItemsSource = ds.Tables[0].DefaultView;
            cid_combo2.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
            cid_combo2.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();



            oracleConnection1.Close();
        }


        private void run_cid() /////////////////////////////
        {

            if (tc.SelectedIndex == 0) // insert window
            {
                dt.Clear();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                dataAdapter1.SelectCommand.CommandText = "select max(cid)+1 as cid from client";
                dataAdapter1.Fill(dt);
                cid.Text = dt.Rows[0]["cid"].ToString();
            }


        }


        // insert client button
        private void Insert(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            InsertCommand.Connection = oracleConnection1;

            char gend;

            if (this.f.IsChecked == true)
                gend = 'F';
            else
                gend = 'M';

            InsertCommand.CommandText = " insert into client (first_name, last_name, gender, address, phone, cid) values (" + "'" +
               this.first.Text + "'" + "," + "'" + this.last.Text + "'" + "," + "'" + gend + "'" + "," + "'" + this.address.Text + "'" + "," + "'" + this.phone.Text + "'" + "," + "'" + this.cid.Text + "'" + " )";

            try
            {
                InsertCommand.ExecuteNonQuery();
                MessageBox.Show("client added succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "client");
                cid_combo.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                cid_combo2.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo2.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo2.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                run_cid(); /////////////////////
                // close ?
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }


        // delete client button
        private void Delete(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            DeleteCommand.Connection = oracleConnection1;

            // if (cid_combo.SelectedIndex >= 0)

            String Value = cid_combo2.Text;

            DeleteCommand.CommandText = " delete from client where " + "cid=" + "'" + Value + "'"; // אולי צריך נקודה פסיק או COMMIT ?

            try
            {
                DeleteCommand.ExecuteNonQuery();
                MessageBox.Show("client deleted succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "client");

                cid_combo.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                cid_combo2.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo2.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo2.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                run_cid(); //////////////////////

                // cid_combo.Items.Refresh();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oracleConnection1.Close();

        }



        // update client button
        private void Update(object sender, RoutedEventArgs e)
        {
            oracleConnection1.Open();
            UpdateCommand.Connection = oracleConnection1;

            char gend;

            if (this.f1.IsChecked == true)
                gend = 'F';
            else
                gend = 'M';

            String Value = cid_combo.Text;


            UpdateCommand.CommandText = " update client set first_name='" + first1.Text + "'" + ",last_name='" + last1.Text + "'" + ", gender='"
                + gend + "'" + ", address='" + address1.Text + "'" + ", phone='" + phone1.Text + "'" + " where cid='" + Value + "'";

            try
            {
                UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("client updated succesfuly");

                ds.Clear();

                dataAdapter2.Fill(ds, "client");
                cid_combo.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                cid_combo2.ItemsSource = ds.Tables[0].DefaultView;
                cid_combo2.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();
                cid_combo2.SelectedValuePath = ds.Tables[0].Columns["first_name"].ToString();

                ///////////////// run bid?

                //cid_combo.Items.Refresh();

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

        private void cid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                MessageBox.Show("שגיאה בהקלדת נתונים \n אנא הכנס רק ספרות ");
                e.Handled = true;
            }
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



    




        private void Clear(object sender, RoutedEventArgs e)
        {
            address.Text = "";
            phone.Text = "";
            first.Text = "";
            last.Text = "";
            cid.Text = "";
        }

        private void Clear1(object sender, RoutedEventArgs e)
        {
            address1.Text = "";
            phone1.Text = "";
            first1.Text = "";
            last1.Text = "";
            cid_combo.SelectedValue = "";
        }






        private void tc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;
           // int t = tc.SelectedIndex;


            //TabItem ti = tc.SelectedItem as TabItem;
            ////MessageBox.Show("This is " + ti.Header + " tab");
            //string t = ti.Header.ToString();


            //switch (t)
            //{
            //    case "Delete":

            //        break;

            //    case "Insert":
            //        if (MessageBox.Show(" ?האם אתה בטוח שברצונך לצאת מחלון זה \n השינויים לא יישמרו", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //        { }
            //        else
            //        {
            //            tc.SelectedIndex = 0; // move to 
            //        }

            //        break;
            //    case "Update":
            //        //if (MessageBox.Show(" ?האם אתה בטוח שברצונך לצאת מחלון זה \n השינויים לא יישמרו", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //        //{ }
            //        //else
            //        //{
            //        //    tc.SelectedIndex = 1; // move to 
            //        //}
            //        break;

            //}



            //if (tc.SelectedIndex == 1)
            //    if (MessageBox.Show(" ?האם אתה בטוח שברצונך לצאת מחלון זה \n השינויים לא יישמרו", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            //    {
            //        string r=tc.se;
            //    }
            //    else
            //    {
            //        tc.SelectedValue = "updTab";
            //        tc.SelectedValue = "Update";
            //        tc.SelectedIndex = 1; // move to Update
            //    }
        }


        private void cid_combo_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                dt.Clear();
                // dt = new DataTable();
                oracleConnection1.Open();
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                String cid = cid_combo.Text.ToString();
                dataAdapter1.SelectCommand.CommandText = "select * from client where cid='" + cid + "'";
                dataAdapter1.Fill(dt);

                first1.Text = dt.Rows[0]["first_name"].ToString();
                last1.Text = dt.Rows[0]["last_name"].ToString();
                address1.Text = dt.Rows[0]["address"].ToString();
                phone1.Text = dt.Rows[0]["phone"].ToString();
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
                MessageBox.Show("You didn't select any client ID");
            }

            oracleConnection1.Close();
        }

        private void first_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }


        public bool CheckOnlyText(string e)
        {
            for (int i = 0; i < e.Length; i++)
                if (e[i] < 'a' || e[i] > 'z' )//|| e[i] > 'A' || e[i] < 'Z')
                    return true;
            return false;
        }

        private void last_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void first1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

        private void last1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = CheckOnlyText(e.Text);
        }

     
        private void phone1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");

            if (!regex.IsMatch(e.Text) == true)
            {
                            e.Handled = true;
            }
        }

        private void phone1_LostFocus(object sender, RoutedEventArgs e)
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



    }







}
