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
    /// Interaction logic for Select.xaml
    /// </summary>
    public partial class Select : Window
    {
        OracleConnection oracleConnection1 = DBSingleton.getConnection();
        OracleDataAdapter dataAdapter1;

        DataTable dt, dt2, dt3, dt4, dt5, dt6, dt7;

        public Select()
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
                dt4 = new DataTable();
                dt5 = new DataTable();
                dt6 = new DataTable();
                dt7 = new DataTable();

                //clear data set
                dt.Clear();

                //prepare data adapter for sql query
                dataAdapter1.SelectCommand.Connection = oracleConnection1;
                //dataAdapter2.SelectCommand.Connection = oracleConnection1;

                //prepare query
                string SQLquery = "select * from branch";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt);
                BdataGrid.ItemsSource = dt.DefaultView;

                SQLquery = "select * from client";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt2);
                CdataGrid.ItemsSource = dt2.DefaultView;

                SQLquery = "select * from worker";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt3);
                WdataGrid.ItemsSource = dt3.DefaultView;

                SQLquery = "select * from transactions";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt4);
                TdataGrid.ItemsSource = dt4.DefaultView;

                SQLquery = "select * from credit_card";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt5);
                CCdataGrid.ItemsSource = dt5.DefaultView;

                SQLquery = "select * from account";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt6);
                AdataGrid.ItemsSource = dt6.DefaultView;

                SQLquery = "select * from account_client";
                dataAdapter1.SelectCommand.CommandText = SQLquery;
                dataAdapter1.Fill(dt7);
                ACdataGrid.ItemsSource = dt7.DefaultView;

                AdataGrid.Items.Refresh();
                BdataGrid.Items.Refresh();
                CdataGrid.Items.Refresh();
                CCdataGrid.Items.Refresh();
                TdataGrid.Items.Refresh();
                WdataGrid.Items.Refresh();

                oracleConnection1.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
