using Applikationen.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Applikationen.MunicipalityFunctions
{
    public class Municipality
    {
        // We define municipalities variables
        public int M_ID { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public List<object> municipalities = new List<object>();
        public List<object> municipalityNames = new List<object>();
        public List<IndustryRestriction> industriesRestrictions = new List<IndustryRestriction>();
        public List<Municipality> municipalityFullList = new List<Municipality>();

        /// <summary>
        /// Allows us to extract a list of municipalities from the database and add them to a combobox.
        /// <para>Kasper, Keemon and Natasha</para>
        /// </summary>
        /// <returns>A list for the combobox with the municipalities from the database</returns>
        public List<ComboBoxItem> GetMunicipalityList()
        {
            // Create a connection to the database
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {
                // Open that connection
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "SELECT * FROM \"Municipalities\"";

                // Use a command to get that data we need
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();

                    List<ComboBoxItem> list = new List<ComboBoxItem>();

                    // While it's reading the data we add it to a new object for each row of the Municipalities table
                    while (dataReader.Read())
                    {
                        municipalityNames.Add(new Municipality() { Name = Convert.ToString(dataReader.GetValue(1)), Region = Convert.ToString(dataReader.GetValue(2)) });
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
        }

        /// <summary>
        /// Extracts all the municipalities from the database for data that we can search through.
        /// <para>Made by Natasha</para>
        /// </summary>
        /// <returns>Gets a list of all the municipalities for binding the data and searching through it.</returns>
        public List<Municipality> GetMunicipalityFullList()
        {
            // Create a connection to the database
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {
                // Open that connection
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "SELECT * FROM \"Municipalities\"";

                // Use a command to get that data we need
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();


                    List<Municipality> list = new List<Municipality>();

                    // While it's reading the data we add it to a new object for each row of the Municipalities table
                    while (dataReader.Read())
                    {
                        municipalityFullList.Add(new Municipality() { Name = Convert.ToString(dataReader.GetValue(1)), Region = Convert.ToString(dataReader.GetValue(2)) });
                    }

                    // We delete the command and close the connection
                    command.Dispose();
                    cnn.Close();
                    return municipalityFullList;
                }
            }
            catch (Exception e)
            {
                List<Municipality> emptyList = new List<Municipality>();
                MessageBox.Show("Database forbindelsen kunne ikke oprettes\n\nSystem fejlbesked:\n" + e.Message);
                return emptyList;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
        }

        /// <summary>
        /// Pulls IndustriesRestrctions for the selected municipality from the database, from the relevant tables.
        /// <para>Made by Natasha, Keemon, and Kasper</para>
        /// </summary>
        /// <param name="municipalityName">The name of the municipality that need to be displayed</param>
        /// <returns>A list with the data which is then displayed in the relevant data grids.</returns>
        public List<IndustryRestriction> DisplayMunicipalityRestrictions(string municipalityName)
        {
            //Database connection string
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            try
            {
                cnn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Gets the correct municipality ID based on the chosen municipality name
                string sql = "SELECT M_ID FROM Municipalities WHERE M_Name LIKE '%" + municipalityName + "%'";
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    var dataReader = command.ExecuteReader();
                    //Data reader gets the value
                    if (dataReader != null && dataReader.HasRows)
                    {
                        Municipality m = new Municipality();

                        while (dataReader.Read())
                        {
                            m.M_ID = Convert.ToInt32(dataReader.GetValue(0));
                        }

                        command.Dispose();
                        dataReader.Close();

                        int M_ID = m.M_ID;
                        //Takes all data from IndustriesRestrictions for the current municipality
                        string sql2 = "SELECT * FROM IndustriesRestrictions WHERE RI_M_ID = " + M_ID;
                        using (SqlCommand command2 = new SqlCommand(sql2, cnn))
                        {

                            var dataReader2 = command2.ExecuteReader();

                            if (dataReader2 != null && dataReader2.HasRows)
                            {
                                while (dataReader2.Read())
                                {
                                    industriesRestrictions.Add(new DatabaseClasses.IndustryRestriction()
                                    {
                                        RI_ID = Convert.ToInt32(dataReader2.GetValue(0)),
                                        RI_Text = Convert.ToString(dataReader2.GetValue(1)),
                                        RI_I_ID = Convert.ToInt32(dataReader2.GetValue(2)),
                                        RI_M_ID = Convert.ToInt32(dataReader2.GetValue(3)),
                                        RI_R_ID = Convert.ToInt32(dataReader2.GetValue(4)),
                                        RI_StartDate = Convert.ToString(dataReader2.GetValue(5)),
                                        RI_EndDate = Convert.ToString(dataReader2.GetValue(6))
                                    });
                                }
                                dataReader2.Close();
                                command2.Dispose();
                                //Checks the industries
                                foreach (DatabaseClasses.IndustryRestriction ir in industriesRestrictions)
                                {
                                    string sql3 = "SELECT * FROM Industries WHERE I_ID LIKE " + ir.RI_I_ID + "";
                                    using (SqlCommand command3 = new SqlCommand(sql3, cnn))
                                    {
                                        var dataReader3 = command3.ExecuteReader();
                                        while (dataReader3.Read())
                                        {
                                            ir.I_Name = Convert.ToString(dataReader3.GetValue(1));
                                            ir.I_Code = Convert.ToString(dataReader3.GetValue(2));
                                            ir.I_Description = Convert.ToString(dataReader3.GetValue(3));
                                        }

                                        dataReader3.Close();
                                        command3.Dispose();
                                    }
                                    //Checks the Restrictions 
                                    string sql4 = "SELECT * FROM Restrictions WHERE R_ID LIKE " + ir.RI_R_ID + "";
                                    using (SqlCommand command4 = new SqlCommand(sql4, cnn))
                                    {
                                        var dataReader4 = command4.ExecuteReader();
                                        while (dataReader4.Read())
                                        {
                                            ir.R_Text = Convert.ToString(dataReader4.GetValue(1));
                                        }

                                        dataReader4.Close();
                                        command4.Dispose();
                                    }
                                }
                            }
                        }
                        cnn.Close();
                    }
                }
                return industriesRestrictions;
            }
            catch (Exception e)
            {
                //Error message if gathering of data fails
                List<DatabaseClasses.IndustryRestriction> emptyList = new List<DatabaseClasses.IndustryRestriction>();
                MessageBox.Show("Fejl ved hentning af restriktioner på industrier\n\nSystem fejlbesked:\n" + e.Message);
                return emptyList;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            }
        }
    }
}
