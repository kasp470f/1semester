using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

// Insert 
// MunicipalityFunctions.Municipality municipality = new MunicipalityFunctions.Municipality();
// municipality.GetMunicipality();
// On page where municipalities need to be called

// Whole section is made by Natasha
namespace Applikationen.MunicipalityFunctions
{
    public class Municipality
    {
        // We define municipalities variables
        public int M_ID { get; set; }
        public string Name { get; set; }

        public List<object> municipalities = new List<object>();
        public List<object> municipalityNames = new List<object>();

        // Method to get municipalities from database
        public void GetMunicipality()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {

                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "SELECT * FROM \"Municipalities\"";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();


                    // While it's reading the data we add it to a new object for each row of the Municipalities table
                    while (dataReader.Read())
                    {
                        municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
                    }

                    // We delete the command and close the connection
                    command.Dispose();
                    cnn.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
            // We open the connection to the database
            //SqlConnection cnn;
            //cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            //cnn.Open();

            //// We create the command and execute it
            //SqlCommand command;
            //SqlDataAdapter adapter = new SqlDataAdapter();

            //string sql = "SELECT * FROM \"Municipalities\"";

            //command = new SqlCommand(sql, cnn);

            //var dataReader = command.ExecuteReader();


            //// While it's reading the data we add it to a new object for each row of the Municipalities table
            //while (dataReader.Read())
            //{
            //	municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
            //}			

            //// We delete the command and close the connection
            //command.Dispose();
            //cnn.Close();
        }

        // Keemon & Natasha
        public List<ComboBoxItem> GetMunicipalityList()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {

                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "SELECT * FROM \"Municipalities\"";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();


                    List<ComboBoxItem> list = new List<ComboBoxItem>();

                    // While it's reading the data we add it to a new object for each row of the Municipalities table
                    while (dataReader.Read())
                    {
                        municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
                        municipalityNames.Add(new Municipality() { Name = Convert.ToString(dataReader.GetValue(1)) });
                    }
                    foreach (Municipality municipality in municipalityNames)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = municipality.Name;
                        list.Add(item);
                    };

                    // We delete the command and close the connection
                    command.Dispose();
                    cnn.Close();
                    return list;
                }
            }
            catch (Exception e)
            {
                List<ComboBoxItem> emptyList = new List<ComboBoxItem>();
                MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
                return emptyList;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
            //// We open the connection to the database
            //SqlConnection cnn;
            //cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            //cnn.Open();

            //// We create the command and execute it
            //SqlCommand command;
            //SqlDataAdapter adapter = new SqlDataAdapter();

            //string sql = "SELECT * FROM \"Municipalities\"";

            //command = new SqlCommand(sql, cnn);

            //var dataReader = command.ExecuteReader();

            //List<ComboBoxItem> list = new List<ComboBoxItem>();

            //// While it's reading the data we add it to a new object for each row of the Municipalities table
            //while (dataReader.Read())
            //{
            //	municipalities.Add(new Municipality() { M_ID = Convert.ToInt32(dataReader.GetValue(0)), Name = Convert.ToString(dataReader.GetValue(1)) });
            //	municipalityNames.Add(new Municipality() { Name = Convert.ToString(dataReader.GetValue(1)) });
            //}
            //foreach (Municipality municipality in municipalityNames)
            //{
            //	ComboBoxItem item = new ComboBoxItem();
            //	item.Content = municipality.Name;
            //	list.Add(item);
            //};
            //// We delete the command and close the connection
            //command.Dispose();
            //cnn.Close();

            //return list;
        }
    }
}
