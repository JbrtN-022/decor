using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace декор.MyClass
{
    internal class ConnectionBD
    {
        public static string connectionString = @"Database = up_02_2_1; Data Source = localhost; user=root; password=qwerty; charset =utf8;";
        static public MySqlCommand mySqlCommand;
        static public MySqlConnection MySqlConnection;
        static public MySqlDataAdapter mySqlDataAdapter;
        public static DataTable dtCardList = new DataTable();
        public static DataTable dtTipProductCombobox = new DataTable(); 
             public static DataTable dtSizePackagingCombobox = new DataTable();
        public static DataTable dtMaterialInProduct = new DataTable();
        public static DataTable dtNameProductCombox = new DataTable();
        public static bool ConnectBd()
        {
            try
            {
                MySqlConnection = new MySqlConnection(connectionString);
                MySqlConnection.Open();
                mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = MySqlConnection;
                mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
               
                return true;

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public static void CloseBD()
        {
            MySqlConnection.Close();
        }

        
        
        
        
    }
}
