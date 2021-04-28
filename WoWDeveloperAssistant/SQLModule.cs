using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WoWDeveloperAssistant
{
    public static class SQLModule
    {
        public static DataSet DatabaseSelectQuery(string query)
        {
            DataSet dataSet = new DataSet();
            MySqlConnection sqlConnection = new MySqlConnection
            {
                ConnectionString = "server = " + Properties.Settings.Default.Host + "; port = " + Properties.Settings.Default.Port + "; user id = " + Properties.Settings.Default.Username + "; password = " + Properties.Settings.Default.Password + "; database = " + Properties.Settings.Default.WorldDatabase
            };

            try
            {
                sqlConnection.Open();
                MySqlCommand myCommand = new MySqlCommand(query, sqlConnection);
                MySqlDataAdapter DataAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                DataAdapter.Fill(dataSet, "table");
                return dataSet;
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return null;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public static DataSet HotfixSelectQuery(string query)
        {
            DataSet dataSet = new DataSet();
            MySqlConnection sqlConnection = new MySqlConnection
            {
                ConnectionString = "server = " + Properties.Settings.Default.Host + "; port = " + Properties.Settings.Default.Port + "; user id = " + Properties.Settings.Default.Username + "; password = " + Properties.Settings.Default.Password + "; database = " + Properties.Settings.Default.HotfixDatabase
            };

            try
            {
                sqlConnection.Open();
                MySqlCommand myCommand = new MySqlCommand(query, sqlConnection);
                MySqlDataAdapter DataAdapter = new MySqlDataAdapter
                {
                    SelectCommand = myCommand
                };

                DataAdapter.Fill(dataSet, "table");
                return dataSet;
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return null;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public static bool TryConnectToDB(string hostName, string port, string userName, string password, string databaseName)
        {
            MySqlConnection sqlConnection = new MySqlConnection
            {
                ConnectionString = "server = " + hostName + "; port = " + port + "; user id = " + userName + "; password = " + password + "; database = " + databaseName
            };

            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database please re-enter login information." + Environment.NewLine + myerror.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
