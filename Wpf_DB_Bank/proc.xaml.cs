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

namespace Wpf_DB_Bank
{
    /// <summary>
    /// Interaction logic for proc.xaml
    /// </summary>
    public partial class proc : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();

        OracleCommand callProcCommand;
        OracleCommand callFuncComm;
        OracleDataAdapter dataAdapter2;
        DataSet ds = new DataSet();

        public proc()
        {
            InitializeComponent();

            oracleConnection1.Open();
            callProcCommand = new OracleCommand();
            callFuncComm = new OracleCommand();


            dataAdapter2 = new OracleDataAdapter();
            dataAdapter2.SelectCommand = new OracleCommand();
            //prepare query
            string SQLquery = "select cid from client";

            //prepare data adapter for sql query
            dataAdapter2.SelectCommand.Connection = oracleConnection1;
            dataAdapter2.SelectCommand.CommandText = SQLquery;

            dataAdapter2.Fill(ds, "client");
            cid_combo.ItemsSource = ds.Tables[0].DefaultView;
            cid_combo.DisplayMemberPath = ds.Tables[0].Columns["cid"].ToString();


            oracleConnection1.Close();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            oracleConnection1.Open();

            callProcCommand.Connection = oracleConnection1;

            callProcCommand.CommandType = CommandType.StoredProcedure;
            callProcCommand.CommandText = "raise_limitt";
            callProcCommand.Parameters.Add("cidd", OracleType.NVarChar);
            callProcCommand.Parameters["cidd"].Direction = ParameterDirection.Input;
            callProcCommand.Parameters["cidd"].Value = this.cid_combo.Text.ToString();


            try
            {
                callProcCommand.ExecuteNonQuery();
                MessageBox.Show("limit raised succesfuly");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            oracleConnection1.Open();

            callFuncComm.Connection = oracleConnection1;
            callFuncComm.CommandType = CommandType.StoredProcedure;
            callFuncComm.CommandText = "avg_min_bid";
            callFuncComm.Parameters.Add("bid", OracleType.Number);
            callFuncComm.Parameters["bid"].Direction = ParameterDirection.ReturnValue;
            callFuncComm.ExecuteNonQuery();
            this.ans.Text = callFuncComm.Parameters["bid"].Value.ToString();

            try
            {
                callFuncComm.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            oracleConnection1.Close();
        }
    }
}
