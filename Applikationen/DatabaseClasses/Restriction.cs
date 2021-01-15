using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace Applikationen.DatabaseClasses
{
    // Section is made by Natasha, and modified by Keemon and Kasper
    public class Restriction
    {
        // We define restriction variables
        public int R_ID { get; set; }
        public string R_Text { get; set; }

        // We create a list to store the restrictions from the database in
        List<Restriction> restrictions = new List<Restriction>();

        // Method to get restrictions from database
        public List<Restriction> GetRestriction()
        {

            // We open the connection to the database
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "SELECT * FROM Restrictions";

                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();

                    // While it's reading the data we add it to a new object for each row of the Restrictions table
                    while (dataReader.Read())
                    {
                        restrictions.Add(new Restriction() { R_ID = Convert.ToInt32(dataReader.GetValue(0)), R_Text = Convert.ToString(dataReader.GetValue(1)) });
                    }

                    // We delete the command and close the connection
                    command.Dispose();
                    cnn.Close();

                    return restrictions;
                }
            }
            catch (Exception e)
            {
                List<Restriction> emptyList = new List<Restriction>();
                MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
                return emptyList;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
        }
    }
}
